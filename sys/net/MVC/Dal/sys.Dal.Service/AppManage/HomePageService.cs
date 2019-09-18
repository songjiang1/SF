using sys.Dal.Entity.PublicInfoManage;
using sys.Bll.Repository;
using sys.Util;
using sys.Util.Extension;
using sys.Util.WebControl;
using System.Collections.Generic;
using sys.Dal.IService.HomeInfoManage; 
using System.Text;
using System.Data.Common;
using sys.Bll;
using System.Data; 
using System;
using sys.Dal.Entity.AppManage;
using sys.Dal.IService.AppManage;

namespace sys.Dal.Service.AppManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2019.05.22 10:40
    /// 描 述：  主页
    /// </summary>
    public class HomePageService : RepositoryFactory<HomePageEntity>, IHomePageService
    {
        #region 获取数据
        /// <summary>
        ///  列表 分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param> 
        /// <returns></returns> 
       public IEnumerable<HomePageEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<HomePageEntity>();
            var newTime = DateTime.Now;
            var queryParam = queryJson.ToJObject();
            if (!queryParam["Title"].IsEmpty())
            {
                string Title = queryParam["Title"].ToString();
                expression = expression.And(t => t.Title.Contains(Title));
            } 
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        ///  列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <returns></returns>
        public IEnumerable<HomePageEntity> GetList()
        {
            var expression = LinqExtensions.True<HomePageEntity>();
            return this.BaseRepository().IQueryable(expression);
        }
        /// <summary>
        /// 公告实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public HomePageEntity GetEntity(string keyValue)
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
        /// <param name="HomePageEntity">公告实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, HomePageEntity HomePageEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                HomePageEntity.Modify(keyValue);
                this.BaseRepository().Update(HomePageEntity);
            }
            else
            {
                HomePageEntity.Create();
                this.BaseRepository().Insert(HomePageEntity);
            }
        }
        #endregion
    }
}
