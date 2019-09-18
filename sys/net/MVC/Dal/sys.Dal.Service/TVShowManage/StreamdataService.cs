using sys.Dal.Entity.TVShowManage;
using sys.Dal.IService.TVShowManage;
using sys.Bll.Repository;
using sys.Util;
using sys.Util.Extension;
using sys.Util.WebControl;
using System.Collections.Generic;
using System;
using sys.Application.Code;

namespace sys.Dal.Service.TVShowManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.12.7 16:40
    /// 描 述：电子会议
    /// </summary>
    public class StreamdataService : RepositoryFactory<StreamdataEntity>, IStreamdataService
    { 
        #region 获取数据
        /// <summary>
        /// 会议列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<StreamdataEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<StreamdataEntity>();
            var newTime = DateTime.Now;
            var queryParam = queryJson.ToJObject(); 
            if (!queryParam["RefId"].IsEmpty())
            {
                string RefId = queryParam["RefId"].ToString();
                expression = expression.And(t => t.RefId == RefId);  
            }
            
            return this.BaseRepository().FindList(expression, pagination);
        }
        public IEnumerable<StreamdataEntity> GetList()
        {
            var expression = LinqExtensions.True<StreamdataEntity>();
            expression = expression.And(t => t.EnabledMark ==1);
            return this.BaseRepository().IQueryable(expression);
        }
        /// <summary>
        /// 会议实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public StreamdataEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除会议
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存会议表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="newsEntity">会议实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, StreamdataEntity streamdataEntity)
        {

            if (!string.IsNullOrEmpty(keyValue))
            {
                streamdataEntity.Modify(keyValue);
                this.BaseRepository().Update(streamdataEntity);
            }
            else
            { 
                streamdataEntity.Create();
                this.BaseRepository().Insert(streamdataEntity);
            }
        }

        /// <summary>
        /// 更新浏览量
        /// </summary>
        /// <param name="keyValue"></param>
        public void PvPlusOne(string keyValue)
        {
            StreamdataEntity streamdataEntity = new StreamdataEntity();
            streamdataEntity = GetEntity(keyValue); 
            streamdataEntity.PV = streamdataEntity.PV.IsNull() + 1;
            this.BaseRepository().Update(streamdataEntity);
        }
         
        #endregion
    }
}
