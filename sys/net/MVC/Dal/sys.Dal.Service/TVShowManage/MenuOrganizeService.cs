
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
    public class MenuOrganizeService : RepositoryFactory<MenuOrganizeEntity>, IMenuOrganizeService
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
        public IEnumerable<MenuOrganizeEntity> GetList(string organizeId)
        {
            var expression = LinqExtensions.True<MenuOrganizeEntity>();
            expression = expression.And(t => t.OrganizeId == organizeId);
            return this.BaseRepository().IQueryable(expression).OrderBy(t => t.SortCode).ToList();

        }
        public IEnumerable<MenuOrganizeEntity> GetList()
        {
            var expression = LinqExtensions.True<MenuOrganizeEntity>();
            return this.BaseRepository().IQueryable(expression).OrderBy(t => t.SortCode).ToList();
        }
        /// <summary>
        /// 功能实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public MenuOrganizeEntity GetEntity(string keyValue)
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
        public bool ExistEnCode(string enCode, string keyValue, string organizeId)
        {
            var expression = LinqExtensions.True<MenuOrganizeEntity>();
            expression = expression.And(t => t.EnCode == enCode && t.OrganizeId == organizeId);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.Id != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        /// <summary>
        /// 功能名称不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistFullName(string fullName, string keyValue, string organizeId)
        {
            var expression = LinqExtensions.True<MenuOrganizeEntity>();
            expression = expression.And(t => t.FullName == fullName && t.OrganizeId == organizeId);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.Id != keyValue);
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
                int count = db.IQueryable<MenuOrganizeEntity>(t => t.ParentId == keyValue).Count();
                if (count > 0)
                {
                    throw new Exception("当前所选数据有子节点数据！");
                }
                db.Delete<MenuOrganizeEntity>(keyValue);

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
        /// <param name="MenuOrganizeEntity">功能实体</param>
        /// <param name="MenuOrganizeButtonList">按钮实体列表</param>
        /// <param name="MenuOrganizeColumnList">视图实体列表</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, MenuOrganizeEntity menuOrganizeEntity)
        {

            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    menuOrganizeEntity.Modify(keyValue);
                    this.BaseRepository().Update(menuOrganizeEntity);
                    var expression = LinqExtensions.True<MenuOrganizeEntity>().And(t => t.Id != keyValue);
                    this.BaseRepository().Update(expression);
                }
                else
                {
                    menuOrganizeEntity.Create();
                    this.BaseRepository().Insert(menuOrganizeEntity);
                }

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
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                db.Insert(menuOrganizeEntitys);
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        public void Update(List<MenuOrganizeEntity> menuOrganizeEntitys)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                foreach (var item in menuOrganizeEntitys)
                {
                    var menuOrganizeEntity_ = GetEntity(item.Id);
                    if (!string.IsNullOrWhiteSpace(item.Cover))
                    {
                        menuOrganizeEntity_.Cover = item.Cover;
                    }
                    if (!string.IsNullOrWhiteSpace(item.FullName))
                    {
                        menuOrganizeEntity_.FullName = item.FullName;
                    }
                    menuOrganizeEntity_.ArticleMark = item.ArticleMark;
                    db.Update(menuOrganizeEntity_);
                }
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// Update 更新所有字段 包含null
        /// </summary>
        /// <param name="menuOrganizeEntity"></param>
        public void Update1(MenuOrganizeEntity menuOrganizeEntity)
        {
            this.BaseRepository().Update1(menuOrganizeEntity);
        }
        #endregion
    }
}
