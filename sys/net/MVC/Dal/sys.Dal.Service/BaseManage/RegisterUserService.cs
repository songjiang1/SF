using sys.Bll;
using sys.Bll.Repository;
using sys.Util;
using sys.Util.WebControl;
using sys.Util.Extension;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Linq;
using sys.Dal.Entity.BaseManage;
using sys.Dal.IService.BaseManage;
using sys.Dal.IService.AuthorizeManage;
using sys.Dal.Service.AuthorizeManage;
using System;
using sys.Application.Code;

namespace sys.Dal.Service.BaseManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.11.03 10:58
    /// 描 述：用户管理
    /// </summary>
    public class RegisterUserService : RepositoryFactory<RegisterUserEntity>, IRegisterUserService
    {
        //private IAuthorizeService<RegisterUserEntity> iauthorizeservice = new AuthorizeService<RegisterUserEntity>();

        #region 获取数据
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetTable()
        {
            var strSql = new StringBuilder();
            strSql.Append(@"  SELECT  u.*,dt.ItemName  as PositionName
                              FROM    b_register_user u  
                              LEFT JOIN (SELECT   d.ItemName ,  d.ItemValue   
                              FROM    Base_DataItemDetail d
                              LEFT JOIN Base_DataItem i ON i.ItemId = d.ItemId
                              WHERE   i.ItemCode='PositionCategory' and d.EnabledMark = 1  AND d.DeleteMark = 0 )dt ON dt.ItemValue = u.Position 
                            WHERE    u.UserId <> 'System' AND u.EnabledMark = 1 AND u.DeleteMark=0 ");
            return this.BaseRepository().FindTable(strSql.ToString());
        }
        /// <summary>
        /// 用户首字母
        /// </summary>
        /// <returns></returns>
        public DataTable GetInitials()
        {
            var strSql = new StringBuilder();
            strSql.Append(@"   SELECT  u.Initials,IFNULL(COUNT(1),0) as InitialsCount
                               FROM    b_register_user u  
                               WHERE    u.UserId <> 'System' AND u.EnabledMark = 1 AND u.DeleteMark=1
                               GROUP BY u.Initials ");
            return this.BaseRepository().FindTable(strSql.ToString());
        }
        #region MyRegion app展示

        #endregion
        /// <summary>
        /// 用户首字母
        /// </summary>
        /// <returns></returns>
        public DataTable GetInitials(RegisterUserEntity collection)
        {
            var strSql = new StringBuilder();

            strSql.Append(@"    SELECT UPPER(Initials)Initials,IFNULL(COUNT(1),0) as InitialsCount  FROM    b_register_user   b
                     where IFNULL(Initials,'')!=''  AND  b.EnabledMark = 1     ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrWhiteSpace(collection.OrganizeId))
            {
                strSql.Append("  AND  OrganizeId=@OrganizeId");
                parameter.Add(DbParameters.CreateDbParameter("@OrganizeId", collection.OrganizeId));
            }
            if (!collection.RealName.IsEmpty())
            {
                strSql.Append(" AND RealName like @RealName");
                parameter.Add(DbParameters.CreateDbParameter("@RealName", '%' + collection.RealName + '%'));
            }
            if (!collection.Post.IsEmpty())
            {
                strSql.Append(" AND Post IN (@Post) ");
                parameter.Add(DbParameters.CreateDbParameter("@Post", collection.Post));
            }
            if (!collection.PostName.IsEmpty())
            {
                strSql.Append(" AND Post NOT IN (@Post) ");
                parameter.Add(DbParameters.CreateDbParameter("@Post", collection.PostName));
            }
            if (!collection.ParentId.IsEmpty())
            {
                strSql.Append(" AND ParentId=@ParentId) ");
                parameter.Add(DbParameters.CreateDbParameter("@ParentId", collection.ParentId));
            }
            strSql.Append(@"   GROUP BY Initials ORDER BY Initials ASC");
            return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 用户 根据字母加载
        /// </summary>
        /// <returns></returns>
        public DataTable GetInitialsUser(RegisterUserEntity collection, string OrderBy)
        {
            var strSql = new StringBuilder();

            strSql.Append(@"    SELECT UPPER(Initials)Initials,UserId,RealName,HeadIcon,PostName,PositionName ,IFNULL(COUNT(1),0) as InitialsCount  FROM    b_register_user   b
                     where TRIM(IFNULL(Initials,''))!=''  AND  b.EnabledMark = 1     ");
            var parameter = new List<DbParameter>();

            if (!string.IsNullOrWhiteSpace(collection.OrganizeId))
            {
                strSql.Append("  AND  OrganizeId=@OrganizeId");
                parameter.Add(DbParameters.CreateDbParameter("@OrganizeId", collection.OrganizeId));
            }
            if (!collection.RealName.IsEmpty())
            {
                strSql.Append(" AND RealName like @RealName");
                parameter.Add(DbParameters.CreateDbParameter("@RealName", '%' + collection.RealName + '%'));
            }
            if (!collection.Post.IsEmpty())
            {
                strSql.Append(" AND Post IN (@Post) ");
                parameter.Add(DbParameters.CreateDbParameter("@Post", collection.Post));
            }
            if (!collection.PostName.IsEmpty())
            {
                strSql.Append(" AND Post NOT IN (@Post) ");
                parameter.Add(DbParameters.CreateDbParameter("@Post", collection.PostName));
            }
            if (!collection.ParentId.IsEmpty())
            {
                strSql.Append(" AND ParentId=@ParentId) ");
                parameter.Add(DbParameters.CreateDbParameter("@ParentId", collection.ParentId));
            }
            strSql.Append(@"   GROUP BY Initials,UserId,RealName,HeadIcon,PostName,PositionName  ");
            if (!string.IsNullOrWhiteSpace(OrderBy))
            {
                strSql.AppendFormat(" ORDER BY  {0} ", OrderBy);
            }
            else
            {
                strSql.AppendFormat(" ORDER BY  Initials ASC ");
            }

            return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray());
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RegisterUserEntity> GetList()
        {
            var expression = LinqExtensions.True<RegisterUserEntity>();
            expression = expression.And(t => t.UserId != "System" && t.EnabledMark == 1);
            return this.BaseRepository().IQueryable(expression).OrderByDescending(t => t.CreateDate).ToList();
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<RegisterUserEntity> GetPageList(Pagination pagination, string queryJson)
        {

            var expression = LinqExtensions.True<RegisterUserEntity>();
            var queryParam = queryJson.ToJObject();

            //公司主键
            if (!queryParam["organizeId"].IsEmpty())
            {
                string organizeId = queryParam["organizeId"].ToString();
                expression = expression.And(t => t.OrganizeId == organizeId);
            }
            //部门主键
            if (!queryParam["departmentId"].IsEmpty())
            {
                string departmentId = queryParam["departmentId"].ToString();
                expression = expression.And(t => t.DepartmentId.Equals(departmentId));
            }
            //岗位
            if (!queryParam["inPost"].IsEmpty())
            {

                string InPost = queryParam["inPost"].ToString();
                string[] arrPost = InPost.Split(',');
                expression = expression.And(m => arrPost.Contains(m.Post));
            }
            //常委会委员
            if (!queryParam["PostName"].IsEmpty())
            {
                string PostName = queryParam["PostName"].ToString();
                expression = expression.And(t => t.PostName.Equals(PostName));
            }
            //身份
            if (!queryParam["identity"].IsEmpty())
            {
                string InIdentity = queryParam["identity"].ToString();
                string[] arrIdentity = InIdentity.Split(',');
                expression = expression.And(m => arrIdentity.Contains(m.Identity));
            }
            List<int> ints = new List<int> { 1, 2, 3 };
            //部门主键
            if (!queryParam["departmentId"].IsEmpty())
            {
                string departmentId = queryParam["departmentId"].ToString();
                expression = expression.And(t => t.DepartmentId.Equals(departmentId));
            }
        


            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyord = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "Account":            //账户
                        expression = expression.And(t => t.Account.Contains(keyord));
                        break;
                    case "RealName":          //姓名
                        expression = expression.And(t => t.RealName.Contains(keyord));
                        break;
                    case "Mobile":          //手机
                        expression = expression.And(t => t.Mobile.Contains(keyord));
                        break;
                    default:
                        break;
                }
            }
            //expression = expression.And(t => t.UserId != "System");
            //return iauthorizeservice.FindList(expression, pagination);
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 用户列表（ALL）
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllTable()
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  u.UserId ,
                                    u.EnCode ,
                                    u.Account ,
                                    u.RealName ,
                                    u.Gender ,
                                    u.Birthday ,
                                    u.Mobile ,
                                    u.Manager ,
                                    u.OrganizeId,
                                    u.DepartmentId,
                                    o.FullName AS OrganizeName ,
                                    d.FullName AS DepartmentName ,
                                    u.RoleId ,
                                    u.DutyName ,
                                    u.PostName ,
                                    u.EnabledMark ,
                                    u.CreateDate,
                                    u.Description
                            FROM    Base_User u
                                    LEFT JOIN Base_Organize o ON o.OrganizeId = u.OrganizeId
                                    LEFT JOIN Base_Department d ON d.DepartmentId = u.DepartmentId
                            WHERE   1=1");
            strSql.Append(" AND u.UserId <> 'System' AND u.EnabledMark = 1 AND u.DeleteMark=0 order by o.FullName,d.FullName,u.RealName");
            return this.BaseRepository().FindTable(strSql.ToString());
        }
        /// <summary>
        /// 用户列表（导出Excel）
        /// </summary>
        /// <returns></returns>
        public DataTable GetExportList()
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT [Account]
                                  ,[RealName]
                                  ,CASE WHEN Gender=1 THEN '男' ELSE '女' END AS Gender
                                  ,[Birthday]
                                  ,[Mobile]
                                  ,[Telephone]
                                  ,u.[Email]
                                  ,[WeChat]
                                  ,[MSN]
                                  ,u.[Manager]
                                  ,o.FullName AS Organize
                                  ,d.FullName AS Department
                                  ,u.[Description]
                                  ,u.[CreateDate]
                                  ,u.[CreateUserName]
                              FROM Base_User u
                              INNER JOIN Base_Department d ON u.DepartmentId=d.DepartmentId
                              INNER JOIN Base_Organize o ON u.OrganizeId=o.OrganizeId");
            return this.BaseRepository().FindTable(strSql.ToString());
        }
        /// <summary>
        /// 用户实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public RegisterUserEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IEnumerable<RegisterUserEntity> GetUserSearch(RegisterUserEntity user)
        {
            var expression = LinqExtensions.True<RegisterUserEntity>();
            expression = expression.And(t => t.EnabledMark == 1).And(t => t.UserId != "System");
            if (!string.IsNullOrWhiteSpace(user.OpenId))
            {
                expression = expression.And(t => t.OpenId == user.OpenId);
            }
            if (!string.IsNullOrWhiteSpace(user.UserId))
            {
                expression = expression.And(t => t.UserId == user.UserId);
            }
            return this.BaseRepository().IQueryable(expression).OrderByDescending(t => t.CreateDate);
        }
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public RegisterUserEntity CheckLogin(string username)
        {
            var expression = LinqExtensions.True<RegisterUserEntity>();
            expression = expression.And(t => t.Mobile == username);
            return this.BaseRepository().FindEntity(expression);
        }

        /// <summary>
        /// 查询手机号
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public RegisterUserEntity CheckMobile(string Mobile)
        {
            var expression = LinqExtensions.True<RegisterUserEntity>();
            expression = expression.And(t => t.EnabledMark == 1);
            expression = expression.And(t => t.Mobile == Mobile);
            return this.BaseRepository().FindEntity(expression);
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 账户不能重复
        /// </summary>
        /// <param name="account">账户值</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistAccount(string account, string keyValue)
        {
            var expression = LinqExtensions.True<RegisterUserEntity>();
            expression = expression.And(t => t.Account == account);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.UserId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存用户表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="userEntity">用户实体</param>
        /// <returns></returns>
        public string SaveForm(string keyValue, RegisterUserEntity userEntity)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                #region 基本信息
                if (string.IsNullOrWhiteSpace(keyValue))
                {
                    var expression = LinqExtensions.True<RegisterUserEntity>();
                    expression = expression.And(t => t.Mobile == userEntity.Mobile);
                    var vityUserInfo = this.BaseRepository().FindEntity(expression);
                    if (vityUserInfo != null)
                    {
                        //app注册没有填写Account
                        //if (vityUserInfo.Account == userEntity.Account)
                        //{
                        //    throw new Exception("账户已被注册");
                        //}
                        if (vityUserInfo.Mobile == userEntity.Mobile&& vityUserInfo.Usource.ToLower().IndexOf("system") == -1)
                        {
                            throw new Exception("手机已被注册");
                        }

                    }
                }
                if (!string.IsNullOrEmpty(keyValue))
                { 
                    userEntity.UserId = keyValue;
                    userEntity.ModifyDate = DateTime.Now;
                    if (!string.IsNullOrWhiteSpace(userEntity.Password))
                    {
                        userEntity.Secretkey = Md5Helper.MD5(CommonHelper.CreateNo(), 16).ToLower();
                        userEntity.Password = Md5Helper.MD5(DESEncrypt.Encrypt(Md5Helper.MD5(userEntity.Password, 32).ToLower(), userEntity.Secretkey).ToLower(), 32).ToLower();

                    }
                    db.Update(userEntity);
                }
                else
                {
                    userEntity.UserId = Guid.NewGuid().ToString();
                    userEntity.CreateDate = DateTime.Now;
                    userEntity.DeleteMark = 0;
                    userEntity.EnabledMark = 1;
                    keyValue = userEntity.UserId;
                    if (!string.IsNullOrWhiteSpace(userEntity.Password))
                    {
                        userEntity.Secretkey = Md5Helper.MD5(CommonHelper.CreateNo(), 16).ToLower();
                        userEntity.Password = Md5Helper.MD5(DESEncrypt.Encrypt(Md5Helper.MD5(userEntity.Password, 32).ToLower(), userEntity.Secretkey).ToLower(), 32).ToLower();

                    }
                    db.Insert(userEntity);

                }
                #endregion

                #region 默认添加 角色、岗位、职位
                //db.Delete<UserRelationEntity>(t => t.IsDefault == 1 && t.UserId == userEntity.UserId);
                //List<UserRelationEntity> userRelationEntitys = new List<UserRelationEntity>();
                ////角色
                //if (!string.IsNullOrEmpty(userEntity.RoleId))
                //{
                //    userRelationEntitys.Add(new UserRelationEntity
                //    {
                //        Category = 2,
                //        UserRelationId = Guid.NewGuid().ToString(),
                //        UserId = userEntity.UserId,
                //        ObjectId = userEntity.RoleId,
                //        CreateDate = DateTime.Now,
                //        CreateUserId = OperatorProvider.Provider.Current().UserId,
                //        CreateUserName = OperatorProvider.Provider.Current().UserName,
                //        IsDefault = 1,
                //    });
                //}
                ////岗位
                //if (!string.IsNullOrEmpty(userEntity.DutyId))
                //{
                //    userRelationEntitys.Add(new UserRelationEntity
                //    {
                //        Category = 3,
                //        UserRelationId = Guid.NewGuid().ToString(),
                //        UserId = userEntity.UserId,
                //        ObjectId = userEntity.DutyId,
                //        CreateDate = DateTime.Now,
                //        CreateUserId = OperatorProvider.Provider.Current().UserId,
                //        CreateUserName = OperatorProvider.Provider.Current().UserName,
                //        IsDefault = 1,
                //    });
                //}
                ////职位
                //if (!string.IsNullOrEmpty(userEntity.PostId))
                //{
                //    userRelationEntitys.Add(new UserRelationEntity
                //    {
                //        Category = 4,
                //        UserRelationId = Guid.NewGuid().ToString(),
                //        UserId = userEntity.UserId,
                //        ObjectId = userEntity.PostId,
                //        CreateDate = DateTime.Now,
                //        CreateUserId = OperatorProvider.Provider.Current().UserId,
                //        CreateUserName = OperatorProvider.Provider.Current().UserName,
                //        IsDefault = 1,
                //    });
                //}
                //db.Insert(userRelationEntitys);
                #endregion

                db.Commit();

                return keyValue;
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="menuOrganizeEntity"></param>
        public void ImportRegistUser(List<RegisterUserEntity> registerUserEntitys)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                db.Insert(registerUserEntitys);
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 修改用户登录密码
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="Password">新密码（MD5 小写）</param>
        public void RevisePassword(string keyValue, string Password)
        {
            RegisterUserEntity userEntity = GetEntity(keyValue);
            userEntity.UserId = keyValue;
            userEntity.Secretkey = Md5Helper.MD5(CommonHelper.CreateNo(), 16).ToLower();
            userEntity.Password = Md5Helper.MD5(DESEncrypt.Encrypt(Password, userEntity.Secretkey).ToLower(), 32).ToLower();
            this.BaseRepository().Update(userEntity);
        }
        /// <summary>
        /// 忘记密码
        /// </summary>
        /// <param name="Mobile"></param>
        /// <param name="Verify"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public int ForgetPassword(string Mobile, string Password)
        {
            var expression = LinqExtensions.True<RegisterUserEntity>();
            expression = expression.And(t => t.Mobile == Mobile);
            var vityUserInfo = this.BaseRepository().FindEntity(expression);
            if (vityUserInfo != null)
            {
                vityUserInfo.Secretkey = Md5Helper.MD5(CommonHelper.CreateNo(), 16).ToLower();
                vityUserInfo.Password = Md5Helper.MD5(DESEncrypt.Encrypt(Password, vityUserInfo.Secretkey).ToLower(), 32).ToLower();
                return this.BaseRepository().Update(vityUserInfo);
            }
            return -1;
        }

        /// <summary>
        /// 修改头像
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="HeadIcon"></param>
        /// <returns></returns>
        public int UpdateHeadIcon(string keyValue, string HeadIcon)
        {
            RegisterUserEntity userEntity = GetEntity(keyValue);
            userEntity.UserId = keyValue;
            userEntity.HeadIcon = HeadIcon;
            return this.BaseRepository().Update(userEntity);
        }
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="audit"> 审核结果1，通过，2未通过</param>
        /// <param name="verifyDescribe">备注 </param>
        public void SaveReviseAudit(string keyValue, int audit, string verifyDescribe)
        {
            RegisterUserEntity userEntity = GetEntity(keyValue);
            userEntity.UserId = keyValue;
            userEntity.VerifyMark = audit;
            userEntity.VerifyDescribe = verifyDescribe;

            this.BaseRepository().Update(userEntity);
        }
        /// <summary>
        /// 设置上级
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="parentId"> 父级</param> 
        public void UpadteParentId(string keyValue, string parentId, string placeholder, string oldUserId="")
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                //List<RegisterUserEntity> list = new List<RegisterUserEntity>();
                if (!string.IsNullOrWhiteSpace(oldUserId))
                {
                    RegisterUserEntity item = GetEntity(oldUserId);
                    item.ParentId = string.Empty;
                    item.Placeholder = string.Empty;
                    //list.Add(item);
                    db.Update(item);
                }

                RegisterUserEntity userEntity = GetEntity(keyValue);
                userEntity.ParentId = parentId;
                userEntity.Placeholder = placeholder;
                //list.Add(userEntity);
                db.Update(userEntity);
                db.Commit();

            }
            catch (Exception ex)
            {
                db.Rollback();
                throw;
            }
        }

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="State">状态：1-启动；0-禁用</param>
        public void UpdateState(string keyValue, int State)
        {
            RegisterUserEntity userEntity = GetEntity(keyValue);
            userEntity.Modify(keyValue);
            userEntity.EnabledMark = State;
            this.BaseRepository().Update(userEntity);
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userEntity">实体对象</param>
        public void UpdateEntity(RegisterUserEntity userEntity)
        {
            this.BaseRepository().Update(userEntity);
        }

        /// <summary>
        /// 修改用户简介
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="description"> 用户简介</param> 
        public void UpadteDescription(string keyValue, string description)
        {
            RegisterUserEntity userEntity = GetEntity(keyValue);
            userEntity.Description = description;
            this.BaseRepository().Update(userEntity);
        }
        #endregion
    }
}
