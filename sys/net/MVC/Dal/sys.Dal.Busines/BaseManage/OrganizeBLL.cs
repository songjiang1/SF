using sys.Dal.Entity.BaseManage;
using sys.Dal.IService.BaseManage;
using sys.Dal.Service.BaseManage;
using sys.Cache.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using sys.Util;

namespace sys.Dal.Busines.BaseManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.11.02 14:27
    /// 描 述：机构管理
    /// </summary>
    public class OrganizeBLL
    {
        private IOrganizeService service = new OrganizeService();
        /// <summary>
        /// 缓存key
        /// </summary>
        public string cacheKey = "OrganizeCache";

        #region 获取数据
        /// <summary>
        /// 机构列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OrganizeEntity> GetList()
        {
            return service.GetList();
        }
        /// <summary>
        /// 机构实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public OrganizeEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }


        /// <summary>
        /// 获取 下级
        /// </summary>
        /// <param name="list"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// 
        public List<OrganizeEntity> GetTreeBranchList(string keyValue)
        {
            var data = this.GetList().ToList();
            var query = data.Where(p => p.OrganizeId == keyValue).ToList();
            var stmp = "";
            if (query.Count > 0)
            {
                stmp=query[0].OrganizeId;
                //设置为父级
                query[0].ParentId = "0";
            }
            var list2 = query.Concat(GetSonList(data, stmp));
            return list2.ToList();
        }

        /// <summary>
        /// 获取 下级
        /// </summary>
        /// <param name="list"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// 
        public List<OrganizeEntity> GetAllTreeBranchList(string keyValue)
        {
            var data = this.GetList().ToList();
            var query = data.Where(p => p.OrganizeId == keyValue).ToList();
            var list2 = query.Concat(GetSonList(data, query[0].OrganizeId));
            return list2.ToList();
        }
        public List<OrganizeEntity> GetSonList(List<OrganizeEntity> list, string Fid)
        {
            var query = list.Where(p => p.ParentId == Fid).ToList();
            return query.ToList().Concat(query.ToList().SelectMany(t => GetSonList(list, t.OrganizeId))).ToList();
        }

        #endregion

        #region 验证数据
        /// <summary>
        /// 公司名称不能重复
        /// </summary>
        /// <param name="organizeName">公司名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistFullName(string fullName, string keyValue)
        {
            return service.ExistFullName(fullName, keyValue);
        }
        /// <summary>
        /// 外文名称不能重复
        /// </summary>
        /// <param name="enCode">外文名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistEnCode(string enCode, string keyValue)
        {
            return service.ExistEnCode(enCode, keyValue);
        }
        /// <summary>
        /// 中文名称不能重复
        /// </summary>
        /// <param name="shortName">中文名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistShortName(string shortName, string keyValue)
        {
            return service.ExistShortName(shortName, keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除机构
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
        /// 保存机构表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="organizeEntity">机构实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, OrganizeEntity organizeEntity)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(organizeEntity.FullName))
                {
                    if (Str.CheckStringChineseReg(organizeEntity.FullName))
                    {
                        organizeEntity.SimpleSpelling = Str.GetPinYin(organizeEntity.FullName, true).ToUpper();
                    }
                }
                service.SaveForm(keyValue, organizeEntity);
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
