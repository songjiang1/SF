using sys.Cache.Factory;
using sys.Dal.Busines.SystemManage;
using sys.Dal.Busines.TVShowManage;
using sys.Dal.Entity.SystemManage.ViewModel;
using sys.Dal.Entity.TVShowManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace sys.Dal.Cache
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.12.29 9:56
    /// 描 述：数据字典缓存
    /// </summary>
    public class CollectionCache
    {
        private CollectionBLL busines = new CollectionBLL();


        public DataTable GetTableCache()
        {
            CollectionEntity collectionEntity = new CollectionEntity();
            var cacheList = CacheFactory.Cache().GetCache<DataTable>(busines.cacheDtKey);
            if (cacheList == null)
            {
                var data = busines.GetTable(collectionEntity,"");
                CacheFactory.Cache().WriteCache(data, busines.cacheDtKey);
                return data;
            }
            else
            {
                return cacheList;
            }
        }

        public DataTable GetInitialsUserCache()
        {
            CollectionEntity collectionEntity = new CollectionEntity();
            var cacheList = CacheFactory.Cache().GetCache<DataTable>(busines.cacheUserKey);
            if (cacheList == null)
            {
                var data = busines.GetInitialsUser(collectionEntity, "");
                CacheFactory.Cache().WriteCache(data, busines.cacheUserKey);
                return data;
            }
            else
            {
                return cacheList;
            }
        }
        public DataTable GetInitialsTableCache()
        {
            CollectionEntity collectionEntity = new CollectionEntity();
            var cacheList = CacheFactory.Cache().GetCache<DataTable>(busines.cacheInitialsKey);
            if (cacheList == null)
            {
                var data = busines.GetInitials(collectionEntity);
                CacheFactory.Cache().WriteCache(data, busines.cacheInitialsKey);
                return data;
            }
            else
            {
                return cacheList;
            }
        }
        public IEnumerable<CollectionEntity> GetListCache()
        {
            var cacheList = CacheFactory.Cache().GetCache<IEnumerable<CollectionEntity>>(busines.cacheListKey);
            if (cacheList == null)
            {
                var data = busines.GetList();
                CacheFactory.Cache().WriteCache(data, busines.cacheListKey);
                return data;
            }
            else
            {
                return cacheList;
            }
        }


       

        public DataTable GetTable(CollectionEntity collection, string OrderBy)
        {
            var data = this.GetTableCache();
            return data;
        }
        /// <summary>
        /// 用户首字母
        /// </summary>
        /// <returns></returns>
        public DataTable GetInitials(CollectionEntity collection)
        {
            var data = this.GetInitialsTableCache();
            return data;
        }
        /// <summary>
        /// 用户首字母
        /// </summary>
        /// <returns></returns>
        public DataTable GetInitialsUser(CollectionEntity collection, string OrderBy)
        {
            var data = this.GetInitialsUserCache();
            return data;
        }
        public IEnumerable<CollectionEntity> GetUserSearch(CollectionEntity collection)
        {
            var data = this.GetListCache();
            return data;
        }
    }
}
