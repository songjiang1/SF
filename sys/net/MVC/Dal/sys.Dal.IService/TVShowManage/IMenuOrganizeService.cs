
using sys.Dal.Entity.TVShowManage;
using System.Collections.Generic;

namespace sys.Dal.IService.TVShowManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.10.27 09:16
    /// 描 述：系统功能
    /// </summary>
    public interface IMenuOrganizeService
    {
        #region 获取数据
        /// <summary>
        /// 获取最大编号
        /// </summary>
        /// <returns></returns>
        int GetSortCode();
        /// <summary>
        /// 功能列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<MenuOrganizeEntity> GetList(string organizeId);

        /// <summary>
        /// 功能列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<MenuOrganizeEntity> GetList();
        /// <summary>
        /// 功能实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        MenuOrganizeEntity GetEntity(string keyValue);
        #endregion

        #region 验证数据
        /// <summary>
        /// 功能编号不能重复
        /// </summary>
        /// <param name="enCode">编号</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        bool ExistEnCode(string enCode, string keyValue, string organizeId);
        /// <summary>
        /// 功能名称不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        bool ExistFullName(string fullName, string keyValue, string organizeId);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除功能
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="MenuOrganizeEntity">功能实体</param> 
        /// <returns></returns>
        void SaveForm(string keyValue, MenuOrganizeEntity MenuOrganizeEntity);

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="menuOrganizeEntity"></param>
        void InsertList(List<MenuOrganizeEntity> menuOrganizeEntitys);

        /// <summary>
        /// 批量更新数据
        /// </summary>
        /// <param name="menuOrganizeEntity"></param>
        void Update(List<MenuOrganizeEntity> menuOrganizeEntitys);

        /// <summary>
        /// Update 更新所有字段 包含null
        /// </summary>
        /// <param name="menuOrganizeEntity"></param>
        void Update1(MenuOrganizeEntity menuOrganizeEntity);
        #endregion
    }
}
