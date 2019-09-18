
using sys.Bll.Repository;
using sys.Dal.Entity.AuthorizeManage;
using sys.Dal.Entity.TVShowManage;
using sys.Dal.IService.TVShowManage;
using sys.Util.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sys.Dal.Service.TVShowManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.10.27 09:16
    /// 描 述：系统功能
    /// </summary>
    public class MenuService : RepositoryFactory<MenuEntity>, IMenuService
    {
        #region 获取数据
        /// <summary>
        /// 获取最大编号
        /// </summary>
        /// <returns></returns>
        public int GetSortCode()
        {
            int sortCode = this.BaseRepository().IQueryable().Max(t => t.SortCode).ToInt();
            if (!string.IsNullOrEmpty(sortCode.ToString()))
            {
                return sortCode + 1;
            }
            return 100001;
        }
        /// <summary>
        /// 功能列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MenuEntity> GetList()
        { 

            var expression = LinqExtensions.True<MenuEntity>();
            return this.BaseRepository().IQueryable(expression).OrderBy(t => t.SortCode).ToList();

        }
        /// <summary>
        /// 功能实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public MenuEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
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
            var expression = LinqExtensions.True<MenuEntity>();
            expression = expression.And(t => t.EnCode == enCode);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.MenuId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        /// <summary>
        /// 功能名称不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistFullName(string fullName, string keyValue)
        {
            var expression = LinqExtensions.True<MenuEntity>();
            expression = expression.And(t => t.FullName == fullName);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.MenuId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除功能
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                int count = db.IQueryable<MenuEntity>(t => t.ParentId == keyValue).Count();
                int organizecount = db.IQueryable<MenuOrganizeEntity>(t => t.MenuId == keyValue).Count();
                if (count > 0)
                {
                    throw new Exception("当前所选数据有子节点数据！");
                }
                if (organizecount > 0)
                {
                    throw new Exception("当前所选数据正在使用！");
                }
                db.Delete<MenuEntity>(keyValue);
                db.Delete<MenuOrganizeEntity>(t => t.MenuId.Equals(keyValue));  
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="MenuEntity">功能实体</param>
        /// <param name="MenuButtonList">按钮实体列表</param>
        /// <param name="MenuColumnList">视图实体列表</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, MenuEntity menuEntity)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    menuEntity.Modify(keyValue);
                    this.BaseRepository().Update(menuEntity);
                }
                else
                {
                    menuEntity.Create();
                    this.BaseRepository().Insert(menuEntity);
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
