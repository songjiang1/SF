using sys.Cache.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sys.Dal.Busines.TVShowManage;
using sys.Dal.Entity.TVShowManage; 

namespace sys.Dal.Cache
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2016.3.4 9:56
    /// 描 述：组织架构缓存
    /// </summary>
    public class  MenuOrganizeCach
    {
        private MenuOrganizeBLL busines = new MenuOrganizeBLL();

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public List<MenuOrganizeEntity> GetList(string organizeId, string parentId = "")
        {
            var cacheList = CacheFactory.Cache().GetCache<List<MenuOrganizeEntity>>(busines.cacheKey);
            if (cacheList == null)
            {
                var data = busines.GetList(organizeId,parentId);
                CacheFactory.Cache().WriteCache(data, busines.cacheKey);
                return data;
            }
            else
            {
                return cacheList.Where(t=>t.OrganizeId== organizeId).ToList();
            }
        }
        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="MenuOrganizeId">公司Id</param>
        /// <returns></returns>
        public MenuOrganizeEntity GetEntity(string keyValue)
        {
            return busines.GetEntity(keyValue); 
        }
    }
}
