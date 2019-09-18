using sys.Dal.Entity.HomeInfoManage;
using sys.Dal.IService.HomeInfoManage;
using sys.Dal.Service.HomeManage;
using sys.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using sys.Util;

namespace sys.Dal.Busines.HomeInfoManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2019.05.22 11:31
    /// 描 述：邮件分类
    /// </summary>
    public class HomeInfoBLL
    {
        private IHomeInfoService service = new HomeInfoService();

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
            return service.GetPageList(pagination, queryJson, out pcount);
        }
        /// <summary>
        ///  实体列表
        /// </summary>
        /// <param name="homeInfoEntity">查询参数</param>
        /// <returns></returns> 
        public DataTable GetList(HomeInfoEntity homeInfoEntity)
        {
            return service.GetList(homeInfoEntity);
        }
        /// <summary>
        /// 获取功能实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public HomeInfoEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        #endregion



        #region 提交数据
        /// <summary>
        /// 删除功能
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
        /// 保存表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="HomeInfoEntity">功能实体</param> 
        /// <returns></returns>
        public void SaveForm(string keyValue, HomeInfoEntity homeInfoEntity)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(homeInfoEntity.Content))
                {
                    homeInfoEntity.Content = WebHelper.HtmlEncode(homeInfoEntity.Content);
                }
                service.SaveForm(keyValue, homeInfoEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
