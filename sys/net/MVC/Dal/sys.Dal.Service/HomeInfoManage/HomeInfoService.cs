using sys.Dal.Entity.PublicInfoManage;
using sys.Dal.IService.PublicInfoManage;
using sys.Bll.Repository;
using sys.Util;
using sys.Util.Extension;
using sys.Util.WebControl;
using System.Collections.Generic;
using sys.Dal.IService.HomeInfoManage;
using sys.Dal.Entity.HomeInfoManage;
using System.Text;
using System.Data.Common;
using sys.Bll;
using System.Data;
using sys.Dal.IService.AuthorizeManage;

namespace sys.Dal.Service.HomeManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2019.05.22 10:40
    /// 描 述：  主页
    /// </summary>
    public class HomeInfoService : RepositoryFactory<HomeInfoEntity>, IHomeInfoService
    {
        #region 获取数据
        /// <summary>
        ///  列表 分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <param name="pcount">总页码数</param>
        /// <returns></returns>
        public DataTable GetPageList(Pagination pagination, string queryJson, out int pcount)
        {
            //var expression = LinqExtensions.True<HomeInfoEntity>();
            //var queryParam = queryJson.ToJObject();
            //if (!queryParam["OrganizeId"].IsEmpty())
            //{
            //    string OrganizeId = queryParam["OrganizeId"].ToString();
            //    expression = expression.And(t => t.OrganizeId.Equals(OrganizeId));
            //}
            //if (!queryParam["Title"].IsEmpty())
            //{
            //    string Title = queryParam["Title"].ToString();
            //    expression = expression.And(t => t.Title.Contains(Title));
            //}
            //if (!queryParam["Category"].IsEmpty())
            //{
            //    string Category = queryParam["Category"].ToString();
            //    expression = expression.And(t => t.Category == Category);
            //}
            //return this.BaseRepository().FindList(expression, pagination);

            string OrganizeId = "";
            string Title = "";
            string Category = "";
            var queryParam = queryJson.ToJObject();

            //DbParameter[] parameter =
            //{
            //    DbParameters.CreateDbParameter("@OrganizeId",OrganizeId),
            //    DbParameters.CreateDbParameter("@Title",Title),
            //    DbParameters.CreateDbParameter("@Category",Category)
            //};
            var parameter = new List<DbParameter>();
            var strSql = new StringBuilder();
            strSql.Append(@" select * from(select a.Id,a.Category,a.Cover,a.CoverLink,a.CreateDate,a.CreateUserId,
            b.RealName as CreateUserName,a.Description,a.HaveSmallGraph,a.IsPublished,a.IsShow,
            a.ModifyDate,a.ModifyUserId,c.RealName as ModifyUserName,a.OrganizeId,d.FullName as OrganizeName,
            a.SortCode,a.Title,e.ItemName as CategoryName
             from b_home_info as a
            left join base_user b on b.UserId=a.CreateUserId
            left join base_user c on c.UserId=a.ModifyUserId
            left join base_organize d on d.OrganizeId=a.OrganizeId
            left join base_dataitemdetail e on e.ItemValue=a.Category
            )t WHERE 1=1");
            if (!queryParam["OrganizeId"].IsEmpty())
            {
                OrganizeId = queryParam["OrganizeId"].ToString();
                strSql.Append("  AND  OrganizeId=@OrganizeId");
                parameter.Add(DbParameters.CreateDbParameter("@OrganizeId", OrganizeId));
            }
            if (!queryParam["Title"].IsEmpty())
            {
                Title = queryParam["Title"].ToString();
                strSql.Append("  AND  Title like @Title");
                parameter.Add(DbParameters.CreateDbParameter("@Title", '%' + Title + '%'));
            }
            if (!queryParam["Category"].IsEmpty())
            {
                Category = queryParam["Category"].ToString();
                strSql.Append("  AND  Category=@Category");
                parameter.Add(DbParameters.CreateDbParameter("@Category", Category));
            }
            strSql.Append(@"   ORDER BY  " + pagination.sidx + "  " + pagination.sord + "  ");
            DataTable countDt = this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray());
            if (countDt != null && countDt.Rows.Count > 0)
            {
                pcount = DataHelper.getTotalPage(countDt.Rows.Count, pagination.rows);
                countDt = DataHelper.getPageTable(countDt, pagination.page, pagination.rows);
            }
            else
            {
                pcount = 0;
            }
            return countDt;
            //strSql.Append(" LIMIT " + (pagination.page - 1) * pagination.rows + ", " + pagination.rows + " ");
            //return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        ///  列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <returns></returns>
        public DataTable GetList(HomeInfoEntity homeInfoEntity)
        {
            //var expression = LinqExtensions.True<HomeInfoEntity>();
            //if (!string.IsNullOrWhiteSpace(homeInfoEntity.OrganizeId))
            //{
            //    expression = expression.And(t => t.OrganizeId == homeInfoEntity.OrganizeId);
            //}
            //if (!string.IsNullOrWhiteSpace(homeInfoEntity.Title))
            //{
            //    expression = expression.And(t => t.Title == homeInfoEntity.Title);
            //}
            //if (!string.IsNullOrWhiteSpace(homeInfoEntity.Category))
            //{
            //    expression = expression.And(t => t.Category == homeInfoEntity.Category);
            //}
            //return this.BaseRepository().IQueryable(expression);

            string OrganizeId = "";
            string Title = "";
            string Category = "";

            var parameter = new List<DbParameter>();
            var strSql = new StringBuilder();
            strSql.Append(@" select * from(select a.Id,a.Category,a.Cover,a.CoverLink,a.CreateDate,a.CreateUserId,
            b.RealName as CreateUserName,a.Description,a.HaveSmallGraph,a.IsPublished,a.IsShow,
            a.ModifyDate,a.ModifyUserId,c.RealName as ModifyUserName,a.OrganizeId,d.FullName as OrganizeName,
            a.SortCode,a.Title,e.ItemName as CategoryName
             from b_home_info as a
            left join base_user b on b.UserId=a.CreateUserId
            left join base_user c on c.UserId=a.ModifyUserId
            left join base_organize d on d.OrganizeId=a.OrganizeId
            left join base_dataitemdetail e on e.ItemValue=a.Category)t WHERE 1=1");
            if (!homeInfoEntity.OrganizeId.IsEmpty())
            {
                OrganizeId = homeInfoEntity.OrganizeId;
                strSql.Append("  AND  OrganizeId=@OrganizeId");
                parameter.Add(DbParameters.CreateDbParameter("@OrganizeId", OrganizeId));
            }
            if (!homeInfoEntity.Title.IsEmpty())
            {
                Title = homeInfoEntity.Title;
                strSql.Append("  AND  Title like @Title");
                parameter.Add(DbParameters.CreateDbParameter("@Title", '%' + Title + '%'));
            }
            if (!homeInfoEntity.Category.IsEmpty())
            {
                Category = homeInfoEntity.Category;
                strSql.Append("  AND  Category=@Category");
                parameter.Add(DbParameters.CreateDbParameter("@Category", Category));
            }
            return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 公告实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public HomeInfoEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存公告表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="HomeInfoEntity">公告实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, HomeInfoEntity homeInfoEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                homeInfoEntity.Modify(keyValue);
                this.BaseRepository().Update(homeInfoEntity);
            }
            else
            {
                homeInfoEntity.Create();
                this.BaseRepository().Insert(homeInfoEntity);
            }
        }
        #endregion
    }
}
