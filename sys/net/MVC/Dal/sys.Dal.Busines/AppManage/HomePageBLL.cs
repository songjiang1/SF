using sys.Dal.Entity.AppManage;
using sys.Dal.IService.AppManage;
using sys.Dal.Service.AppManage;
using sys.Util;
using sys.Util.Extension;
using sys.Util.WebControl;
using System;
using System.Collections.Generic;

namespace sys.Dal.Busines.AppManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.12.8 11:31
    /// 描 述：邮件分类
    /// </summary>
    public class HomePageBLL
    {
        private IHomePageService service = new HomePageService();

        #region 获取数据
        public IEnumerable<HomePageEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        
        /// <summary>
        /// 分类列表
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<HomePageEntity> GetList()
        {
            return service.GetList();
        }
        /// <summary>
        /// 分类实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public HomePageEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
      
 
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="emailCategoryEntity">分类实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, HomePageEntity HomePageEntity)
        {
            try
            {
                service.SaveForm(keyValue, HomePageEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        

       
        #endregion
    }
}
