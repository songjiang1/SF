
using sys.Cache.Factory;
using sys.Dal.Entity.TVShowManage;
using sys.Dal.IService.TVShowManage;
using sys.Dal.Service.BaseManage;
using sys.Dal.Service.TVShowManage;
using sys.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace sys.Dal.Busines.TVShowManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.10.27 09:16
    /// 描 述：系统功能
    /// </summary>
    public class MenuOrganizeBLL
    {
        private IMenuOrganizeService service = new MenuOrganizeService();
        private IMenuService menuService = new MenuService();

        public string cacheKey = "MenuOrganizeCache";
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
        public List<MenuOrganizeEntity> GetList(string organizeId,string parentId = "")
        {
            var data = service.GetList(organizeId).ToList();
            if (!string.IsNullOrEmpty(parentId))
            {
                data = data.FindAll(t => t.ParentId == parentId);
            }
            return data;
        }
        public List<MenuOrganizeEntity> GetList()
        { 
            return service.GetList().ToList();
        }
        /// <summary>
        /// 获取功能实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public MenuOrganizeEntity GetEntity(string keyValue)
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
        public bool ExistEnCode(string enCode, string keyValue, string organizeId)
        {
            return service.ExistEnCode(enCode, keyValue, organizeId);
        }
        /// <summary>
        /// 功能名称不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistFullName(string fullName, string keyValue, string organizeId)
        {
            return service.ExistFullName(fullName, keyValue, organizeId);
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
                CacheFactory.Cache().RemoveCache(cacheKey);
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
        /// <param name="MenuOrganizeEntity">功能实体</param> 
        /// <returns></returns>
        public void SaveForm(string keyValue, MenuOrganizeEntity menuOrganizeEntity)
        {
            try
            {
            //    var menu= menuService.GetEntity(menuOrganizeEntity.MenuId);
            //    if (menuOrganizeEntity.LookUrlAddress != menu.LookUrlAddress || menuOrganizeEntity.UrlAddress != menu.UrlAddress)
            //    {
            //        menuOrganizeEntity.LookUrlAddress = menu.LookUrlAddress;
            //        menuOrganizeEntity.UrlAddress = menu.UrlAddress;
            //    }
                if (!string.IsNullOrWhiteSpace(menuOrganizeEntity.ShortName))
                {
                    if (Str.CheckStringChineseReg(menuOrganizeEntity.ShortName))
                    {
                        menuOrganizeEntity.SimpleSpelling = Str.GetPinYin(menuOrganizeEntity.ShortName, true).ToUpper();
                    }
                }
                service.SaveForm(keyValue, menuOrganizeEntity);
                CacheFactory.Cache().RemoveCache(cacheKey);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="menuOrganizeEntity"></param>
        public void InsertList(List<MenuOrganizeEntity> menuOrganizeEntitys)
        {
            try
            {
                service.InsertList(menuOrganizeEntitys);
                CacheFactory.Cache().RemoveCache(cacheKey);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Update(List<MenuOrganizeEntity> menuOrganizeEntitys)
        {
            try
            {
                service.Update(menuOrganizeEntitys);
                CacheFactory.Cache().RemoveCache(cacheKey);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        ///  Update 更新所有字段 包含null
        /// </summary>
        /// <param name="menuOrganizeEntity"></param>
        public void Update1(MenuOrganizeEntity menuOrganizeEntity)
        {
            try
            {
                service.Update1(menuOrganizeEntity);
                CacheFactory.Cache().RemoveCache(cacheKey);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
