﻿using sys.Dal.Busines.BaseManage;
using sys.Dal.Entity.BaseManage;
using sys.Cache.Factory;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace sys.Dal.Cache
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2016.3.4 9:56
    /// 描 述：角色信息缓存
    /// </summary>
    public class RoleCache
    {
        private RoleBLL busines = new RoleBLL();

        /// <summary>
        /// 角色列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RoleEntity> GetList()
        {
            var cacheList = CacheFactory.Cache().GetCache<IEnumerable<RoleEntity>>(busines.cacheKey);
            if (cacheList == null)
            {
                var data = busines.GetList();
                CacheFactory.Cache().WriteCache(data, busines.cacheKey);
                return data;
            }
            else
            {
                return cacheList;
            }
        }
        /// <summary>
        /// 角色列表
        /// </summary>
        /// <param name="organizeId">公司Id</param>
        /// <returns></returns>
        public IEnumerable<RoleEntity> GetList(string organizeId)
        {
            var data = this.GetList();
            if (!string.IsNullOrEmpty(organizeId))
            {
                data = data.Where(t => t.OrganizeId == organizeId || t.IsPublic==1);
            }
            return data;
        }
        /// <summary>
        /// 角色实体
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public RoleEntity GetEntity(string roleId)
        {
            var data = this.GetList();
            if (!string.IsNullOrEmpty(roleId))
            {
                var d = data.Where(t => t.RoleId == roleId).ToList<RoleEntity>();
                if (d.Count > 0)
                {
                    return d[0];
                }
            }
            return new RoleEntity();
        }
    }
}
