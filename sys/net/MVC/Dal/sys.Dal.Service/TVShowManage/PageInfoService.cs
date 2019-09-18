using sys.Dal.Entity.TVShowManage;
using sys.Dal.IService.TVShowManage;
using sys.Bll.Repository;
using sys.Util;
using sys.Util.Extension;
using sys.Util.WebControl;
using System.Collections.Generic;
using System;
using sys.Application.Code;

namespace sys.Dal.Service.TVShowManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.12.7 16:40
    /// 描 述：电子会议
    /// </summary>
    public class PageInfoService : RepositoryFactory<PageInfoEntity>, IPageInfoService
    { 
        #region 获取数据
        /// <summary>
        /// 会议列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<PageInfoEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<PageInfoEntity>();
            var newTime = DateTime.Now;
            var queryParam = queryJson.ToJObject();
            if (!queryParam["OrganizeId"].IsEmpty())
            {
                string OrganizeId = queryParam["OrganizeId"].ToString();
                expression = expression.And(t => t.OrganizeId == OrganizeId);
            }
            if (!queryParam["PageId"].IsEmpty())
            {
                string PageId = queryParam["PageId"].ToString();
                expression = expression.And(t => t.PageId == PageId);  
            }
            if (!queryParam["Name"].IsEmpty())
            {
                string Name = queryParam["Name"].ToString();
                expression = expression.And(t => t.Name.Contains(Name));
            }
            return this.BaseRepository().FindList(expression, pagination);
        }
        public IEnumerable<PageInfoEntity> GetList()
        {
            var expression = LinqExtensions.True<PageInfoEntity>();
            expression = expression.And(t => t.EnabledMark ==1);
            return this.BaseRepository().IQueryable(expression);
        }
        /// <summary>
        /// 会议实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public PageInfoEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除会议
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存会议表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="newsEntity">会议实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, PageInfoEntity pageInfoEntity)
        {

            if (!string.IsNullOrEmpty(keyValue))
            {
                pageInfoEntity.Modify(keyValue);
                this.BaseRepository().Update(pageInfoEntity);
            }
            else
            { 
                pageInfoEntity.Create();
                this.BaseRepository().Insert(pageInfoEntity);
            }
        }

     
        #endregion
    }
}
