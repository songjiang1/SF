using sys.Dal.Entity.TVShowManage;
using sys.Dal.IService.TVShowManage;
using sys.Dal.Service.TVShowManage;
using sys.Util;
using sys.Util.WebControl;
using System;
using System.Collections.Generic;

namespace sys.Dal.Busines.TVShowManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期： 
    /// 描 述： 
    /// </summary>
    public class StreamdataBLL
    {
        private IStreamdataService service = new StreamdataService();

        #region 获取数据
        /// <summary>
        /// 会议列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<StreamdataEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        public IEnumerable<StreamdataEntity> GetList()
        {
            return service.GetList();
        }
        /// <summary>
        /// 会议实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public StreamdataEntity GetEntity(string keyValue)
        {
            StreamdataEntity streamdataEntity = service.GetEntity(keyValue);
            streamdataEntity.Content = WebHelper.HtmlDecode(streamdataEntity.Content);
            return streamdataEntity;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除会议
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
        /// 保存 （新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="newsEntity">实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, StreamdataEntity streamdataEntity)
        {
            try
            {
                streamdataEntity.Content = WebHelper.HtmlEncode(streamdataEntity.Content);
                service.SaveForm(keyValue, streamdataEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 更新浏览量 
        /// </summary>
        /// <param name="keyValue"></param>
        public void PvPlusOne(string keyValue)
        {
            try
            {
                service.PvPlusOne(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
         

      
        #endregion
    }
}
