
using sys.Dal.Entity.TVShowManage;
using sys.Dal.IService.TVShowManage;
using sys.Dal.Service.BaseManage;
using sys.Dal.Service.TVShowManage;
using sys.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using sys.Util.Extension;

namespace sys.Dal.Busines.TVShowManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.10.27 09:16
    /// 描 述：系统功能
    /// </summary>
    public class MenuBLL
    {
        private IMenuService service = new MenuService();
        private MenuOrganizeBLL menuOrganizeBLL = new MenuOrganizeBLL();

        #region 获取数据
        /// <summary>
        /// 获取最大编号
        /// </summary>
        /// <returns></returns>
        public int GetSortCode()
        {
            return service.GetSortCode();
        }
        /// <summary>
        /// 获取功能列表
        /// </summary>
        /// <param name="parentId">父级主键</param>
        /// <returns></returns>
        public List<MenuEntity> GetList(string parentId = "")
        {
            var data = service.GetList().ToList();
            if (!string.IsNullOrEmpty(parentId))
            {
                data = data.FindAll(t => t.ParentId == parentId);
            }
            return data;
        }
        /// <summary>
        /// 获取功能实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public MenuEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 功能编号不能重复
        /// </summary>
        /// <param name="enCode">编号</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistEnCode(string enCode, string keyValue)
        {
            return service.ExistEnCode(enCode, keyValue);
        }
        /// <summary>
        /// 功能名称不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistFullName(string fullName, string keyValue)
        {
            return service.ExistFullName(fullName, keyValue);
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
        /// <param name="MenuEntity">功能实体</param> 
        /// <returns></returns>
        public void SaveForm(string keyValue, MenuEntity menuEntity)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(menuEntity.ShortName))
                {
                    if (Str.CheckStringChineseReg(menuEntity.ShortName))
                    {
                        menuEntity.SimpleSpelling = Str.GetPinYin(menuEntity.ShortName, true).ToUpper();
                    }
                }
                service.SaveForm(keyValue, menuEntity);
                if (!string.IsNullOrWhiteSpace(keyValue))
                {
                    var menuorganize = menuOrganizeBLL.GetList().Where(t => t.MenuId == keyValue);
                    foreach (var item in menuorganize)
                    {
                        //if (menuEntity.LookUrlAddress != item.LookUrlAddress || menuEntity.UrlAddress != item.UrlAddress)
                        //{
                            var organizeEntity = menuOrganizeBLL.GetEntity(item.Id);
                        //organizeEntity.LookUrlAddress =  menuEntity.LookUrlAddress;
                        //organizeEntity.UrlAddress =  menuEntity.UrlAddress;
                        //menuOrganizeBLL.Update1(organizeEntity);
                        organizeEntity.LookUrlAddress =  menuEntity.LookUrlAddress.ToNull();
                        organizeEntity.UrlAddress =   menuEntity.UrlAddress.ToNull();
                        menuOrganizeBLL.SaveForm(item.Id, organizeEntity);
                        //}
                    }
                    
                }
               
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
