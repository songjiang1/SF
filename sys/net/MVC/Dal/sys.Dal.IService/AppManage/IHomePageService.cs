using sys.Dal.Entity;
using sys.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using sys.Dal.Entity.AppManage;

namespace sys.Dal.IService.AppManage
{
    /// <summary>
    /// 版本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2019.3.27
    /// 描 述：系统主页
    /// </summary>
    public interface IHomePageService
    {

        #region 获取数据
        /// <summary>
        /// 主页信息列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param> 
        /// <returns></returns>
        IEnumerable<HomePageEntity> GetPageList(Pagination pagination, string queryJson);

        /// <summary>
        /// 主页信息列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<HomePageEntity> GetList();
        /// <summary>
        /// 主页信息实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        HomePageEntity GetEntity(string keyValue);

        #endregion
        #region 提交数据
        /// <summary>
        /// 提交/修改
        /// </summary>
        ///   <param name="keyValue">主键</param>
        /// <param name="HomeInfo">对象</param>
        void SaveForm(string keyValue, HomePageEntity homePageEntity);

        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        #endregion
    }
}
