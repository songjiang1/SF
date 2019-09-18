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
using System;
using sys.Application.Code;
using sys.Dal.Entity.TVShowManage;
using sys.Dal.IService.TVShowManage;

namespace sys.Dal.Service.TVShowManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.11.03 10:58
    /// 描 述：用户管理
    /// </summary>
    public class CollectionService : RepositoryFactory<CollectionEntity>, ICollectionService
    {

        #region 获取数据
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetTable(CollectionEntity collection, string OrderBy)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"  SELECT  Id,Title,TVShowMenuId,HeadIcon,UserId,Initials, UPPER(Initials)UPInitials,Spelling,Mobile,ParentId,
                              SortCode,Post,Position,WorkUnit,ContactAddress,PV,LikeCount,CommentCount,RepresentName
                              FROM    b_tv_collection b   
                              WHERE   EnabledMark = 1   ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrWhiteSpace(collection.ParentId))
            {
                strSql.Append("  AND (ParentId=@ParentId OR  Id=@ParentId )");
                parameter.Add(DbParameters.CreateDbParameter("@ParentId", collection.ParentId));
            }
            if (!string.IsNullOrWhiteSpace(collection.TVShowMenuId))
            {
                strSql.Append("  AND  TVShowMenuId=@TVShowMenuId");
                parameter.Add(DbParameters.CreateDbParameter("@TVShowMenuId", collection.TVShowMenuId));
            }
            if (!string.IsNullOrWhiteSpace(collection.OrganizeId))
            {
                strSql.Append("  AND  OrganizeId=@OrganizeId");
                parameter.Add(DbParameters.CreateDbParameter("@OrganizeId", collection.OrganizeId));
            }
            if (!collection.Title.IsEmpty())
            {
                strSql.Append(" AND Title like @Title");
                parameter.Add(DbParameters.CreateDbParameter("@Title", '%' + collection.Title + '%'));
            }
            if (!collection.CreateUserId.IsEmpty())
            {
                strSql.Append(" AND  CreateUserId = @CreateUserId");
                parameter.Add(DbParameters.CreateDbParameter("@CreateUserId", collection.CreateUserId));
            }
            if (!collection.UserId.IsEmpty())
            {
                strSql.Append(" AND  UserId = @UserId");
                parameter.Add(DbParameters.CreateDbParameter("@UserId", collection.UserId));
            }
            if (!collection.Category.IsEmpty())
            {
                strSql.Append(" AND  Category = @Category");
                parameter.Add(DbParameters.CreateDbParameter("@Category", collection.Category));
            }
            if (!OrderBy.IsEmpty())
            {
                strSql.Append(" ORDER BY  " + OrderBy);
            }
            return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 用户首字母
        /// </summary>
        /// <returns></returns>
        public DataTable GetInitials(CollectionEntity collection)
        {
            var strSql = new StringBuilder();

            strSql.Append(@"    SELECT UPPER(Initials)Initials,IFNULL(COUNT(1),0) as InitialsCount  FROM    b_tv_collection   b
                     where IFNULL(Initials,'')!=''  AND  b.EnabledMark = 1     ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrWhiteSpace(collection.ParentId))
            {
                strSql.Append("  AND (ParentId=@ParentId OR  Id=@ParentId )");
                parameter.Add(DbParameters.CreateDbParameter("@ParentId", collection.ParentId));
            }
            if (!string.IsNullOrWhiteSpace(collection.TVShowMenuId))
            {
                strSql.Append("  AND  TVShowMenuId=@TVShowMenuId");
                parameter.Add(DbParameters.CreateDbParameter("@TVShowMenuId", collection.TVShowMenuId));
            }
            if (!string.IsNullOrWhiteSpace(collection.OrganizeId))
            {
                strSql.Append("  AND  OrganizeId=@OrganizeId");
                parameter.Add(DbParameters.CreateDbParameter("@OrganizeId", collection.OrganizeId));
            }
            if (!collection.Title.IsEmpty())
            {
                strSql.Append(" AND Title like @Title");
                parameter.Add(DbParameters.CreateDbParameter("@Title", '%' + collection.Title + '%'));
            }
            strSql.Append(@"   GROUP BY Initials ORDER BY Initials ASC");
            return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 用户 根据字母加载
        /// </summary>
        /// <returns></returns>
        public DataTable GetInitialsUser(CollectionEntity collection, string OrderBy)
        {
            var strSql = new StringBuilder();

            strSql.Append(@"    SELECT UPPER(Initials)Initials,UserId,Title,HeadIcon,Post,Position ,IFNULL(COUNT(1),0) as InitialsCount  FROM    b_tv_collection   b
                     where TRIM(IFNULL(Initials,''))!=''  AND  b.EnabledMark = 1     ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrWhiteSpace(collection.ParentId))
            {
                strSql.Append("  AND (ParentId=@ParentId OR  Id=@ParentId )");
                parameter.Add(DbParameters.CreateDbParameter("@ParentId", collection.ParentId));
            }
            if (!string.IsNullOrWhiteSpace(collection.TVShowMenuId))
            {
                strSql.Append("  AND  TVShowMenuId=@TVShowMenuId");
                parameter.Add(DbParameters.CreateDbParameter("@TVShowMenuId", collection.TVShowMenuId));
            }
            if (!string.IsNullOrWhiteSpace(collection.OrganizeId))
            {
                strSql.Append("  AND  OrganizeId=@OrganizeId");
                parameter.Add(DbParameters.CreateDbParameter("@OrganizeId", collection.OrganizeId));
            }
            if (!collection.Title.IsEmpty())
            {
                strSql.Append(" AND Title like @Title");
                parameter.Add(DbParameters.CreateDbParameter("@Title", '%' + collection.Title + '%'));
            }
            strSql.Append(@"   GROUP BY Initials,UserId,Title,HeadIcon,Post,Position  ");
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
        public DataTable GetYiAnUser(CollectionEntity collection, string OrderBy)
        {
            var strSql = new StringBuilder();

            strSql.Append(@"    SELECT UPPER(Initials)Initials,UserId,RepresentName,HeadIcon,Post,Position ,IFNULL(COUNT(1),0) as InitialsCount  FROM    b_tv_collection   b
                     where TRIM(IFNULL(Initials,''))!=''  AND  b.EnabledMark = 1     ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrWhiteSpace(collection.ParentId))
            {
                strSql.Append("  AND (ParentId=@ParentId OR  Id=@ParentId )");
                parameter.Add(DbParameters.CreateDbParameter("@ParentId", collection.ParentId));
            }
            if (!string.IsNullOrWhiteSpace(collection.TVShowMenuId))
            {
                strSql.Append("  AND  TVShowMenuId=@TVShowMenuId");
                parameter.Add(DbParameters.CreateDbParameter("@TVShowMenuId", collection.TVShowMenuId));
            }
            if (!string.IsNullOrWhiteSpace(collection.OrganizeId))
            {
                strSql.Append("  AND  OrganizeId=@OrganizeId");
                parameter.Add(DbParameters.CreateDbParameter("@OrganizeId", collection.OrganizeId));
            }
            if (!collection.Title.IsEmpty())
            {
                strSql.Append(" AND RepresentName like @Title");
                parameter.Add(DbParameters.CreateDbParameter("@Title", '%' + collection.Title + '%'));
            }
            strSql.Append(@"   GROUP BY Initials,UserId,RepresentName,HeadIcon,Post,Position  ");
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
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CollectionEntity> GetList()
        {
            var expression = LinqExtensions.True<CollectionEntity>();
            expression = expression.And(t => t.EnabledMark == 1).And(t => t.DeleteMark == 0);
            return this.BaseRepository().IQueryable(expression).OrderBy(t => t.SortCode).ToList();
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CollectionEntity> GetList(CollectionEntity collectionEntity)
        {
            var expression = LinqExtensions.True<CollectionEntity>();
            expression = expression.And(t => t.EnabledMark == 1).And(t => t.DeleteMark == 0);
            if (!string.IsNullOrWhiteSpace(collectionEntity.OrganizeId))
            {
                expression = expression.And(t => t.OrganizeId == collectionEntity.OrganizeId);
            }
            if (!string.IsNullOrWhiteSpace(collectionEntity.TVShowMenuId))
            {
                expression = expression.And(t => t.TVShowMenuId == collectionEntity.TVShowMenuId);
            }
            if (!string.IsNullOrWhiteSpace(collectionEntity.Category))
            {
                expression = expression.And(t => t.Category == collectionEntity.Category);
            }
            if (!string.IsNullOrWhiteSpace(collectionEntity.RepresentId))
            {
                expression = expression.And(t => t.RepresentId == collectionEntity.RepresentId);
            }
            return this.BaseRepository().IQueryable(expression).OrderBy(t => t.SortCode).ToList();
        }
        /// <summary>
        ///  列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<CollectionEntity> GetPageList(Pagination pagination, string queryJson)
        {

            var expression = LinqExtensions.True<CollectionEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件 ;
            if (!queryParam["ParentId"].IsEmpty())
            {
                string ParentId = queryParam["ParentId"].ToString();
                expression = expression.And(t => t.ParentId == ParentId);
            }
            if (!queryParam["TVShowMenuId"].IsEmpty())
            {
                string TVShowMenuId = queryParam["TVShowMenuId"].ToString();
                expression = expression.And(t => t.TVShowMenuId == TVShowMenuId);
            }
            if (!queryParam["Category"].IsEmpty())
            {
                string Category = queryParam["Category"].ToString();
                expression = expression.And(t => t.Category == Category);
            }
            if (!queryParam["OrganizeId"].IsEmpty())
            {
                string OrganizeId = queryParam["OrganizeId"].ToString();
                expression = expression.And(t => t.OrganizeId == OrganizeId);
            }
            if (!queryParam["Title"].IsEmpty())
            {
                string Title = queryParam["Title"].ToString();
                expression = expression.And(t => t.Title.Contains(Title));
            }
            if (!queryParam["UserId"].IsEmpty())
            {
                string UserId = queryParam["UserId"].ToString();
                expression = expression.And(t => t.UserId == UserId);
            }
            if (!queryParam["CreateUserId"].IsEmpty())
            {
                string CreateUserId = queryParam["UserId"].ToString();
                expression = expression.And(t => t.CreateUserId == CreateUserId);
            }
            if (!queryParam["RepresentId"].IsEmpty())
            {
                string RepresentId = queryParam["RepresentId"].ToString();
                expression = expression.And(t => t.RepresentId == RepresentId);
            }
            if (!queryParam["RepresentName"].IsEmpty())
            {
                string RepresentName = queryParam["RepresentName"].ToString();
                expression = expression.And(t => t.RepresentName.Contains(RepresentName.Trim()));
            }
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 用户列表（ALL）
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllTable()
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  u.Title , 
                                    u.PostName ,
                                    u.EnabledMark ,
                                    u.CreateDate,
                                    u.Description
                            FROM    Base_User u
                                    LEFT JOIN Base_Organize o ON o.OrganizeId = u.OrganizeId
                                    LEFT JOIN Base_Department d ON d.DepartmentId = u.DepartmentId
                            WHERE   1=1");
            strSql.Append(" AND   u.EnabledMark = 1 AND u.DeleteMark=0 order by o.FullName,d.FullName,u.RealName");
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
        public CollectionEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public CollectionEntity GetFormEntity(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            string menuId = queryParam["menuId"].ToString();
            string orgId = queryParam["orgId"].ToString();
            string typeCode = queryParam["typeCode"].ToString();
            string representId = queryParam["representId"].ToString();

            var expression = LinqExtensions.True<CollectionEntity>();
            expression = expression.And(t => t.EnabledMark == 1).And(t => t.DeleteMark == 0)
                .And(t => t.TVShowMenuId == menuId).And(t => t.OrganizeId == orgId)
                .And(t => t.Category == typeCode);
            if (!string.IsNullOrWhiteSpace(representId))
            {
                expression = expression.And(t => t.RepresentId == representId);
            }
            return this.BaseRepository().IQueryable(expression).FirstOrDefault();
        }

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IEnumerable<CollectionEntity> GetUserSearch(CollectionEntity user)
        {
            var expression = LinqExtensions.True<CollectionEntity>();
            expression = expression.And(t => t.EnabledMark == 1).And(t => t.Title != "System");
            if (!string.IsNullOrWhiteSpace(user.OpenId))
            {
                expression = expression.And(t => t.OpenId == user.OpenId);
            }
            if (!string.IsNullOrWhiteSpace(user.TVShowMenuId))
            {
                expression = expression.And(t => t.TVShowMenuId == user.TVShowMenuId);
            }
            return this.BaseRepository().IQueryable(expression).OrderBy(t => t.CreateDate);
        }


        /// <summary>
        /// 查询手机号
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public CollectionEntity CheckMobile(string Mobile)
        {
            var expression = LinqExtensions.True<CollectionEntity>();
            expression = expression.And(t => t.EnabledMark == 1);
            expression = expression.And(t => t.Mobile == Mobile);
            return this.BaseRepository().FindEntity(expression);
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
        public void SaveForm(string keyValue, CollectionEntity collection)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                collection.Modify(keyValue);
                this.BaseRepository().Update(collection);
            }
            else
            {

                collection.Create();
                this.BaseRepository().Insert(collection);
            }
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userEntity">实体对象</param>
        public void UpdateEntity(CollectionEntity userEntity)
        {
            this.BaseRepository().Update(userEntity);
        }

        /// <summary>
        /// 加1操作
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="operatType"></param>
        public void PlusOne(string keyValue, OperatType operatType)
        {
            CollectionEntity collectionEntity = GetEntity(keyValue);
            collectionEntity.Id = keyValue;
            if (operatType == OperatType.PV)
            {
                collectionEntity.PV = collectionEntity.PV.IsNull() + 1;
            }
            if (operatType == OperatType.IsLike)
            {
                collectionEntity.LikeCount = collectionEntity.LikeCount.IsNull() + 1;
            }
            if (operatType == OperatType.CommentCount)
            {
                collectionEntity.CommentCount = collectionEntity.CommentCount.IsNull() + 1;
            }
            this.BaseRepository().Update(collectionEntity);
        }
        #endregion
    }
}
