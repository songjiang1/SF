using Newtonsoft.Json;
using sys.WebApi.Common;
using sys.WebApi.Models;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace sys.WebApi
{
    /// <summary>
    /// 控制器基类
    /// </summary> 
    public abstract class WebApiBaseControllerBase : ApiController
    {
        //private Log _logger;
        ///// <summary>
        ///// 日志操作
        ///// </summary>
        //public Log Logger
        //{
        //    get { return _logger ?? (_logger = LogFactory.GetLogger(this.GetType().ToString())); }
        //}
        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected virtual HttpResponseMessage ToJsonResult(object data)
        {
            //return Content(data.ToJson());
            ResultMsg resultMsg = null;
            resultMsg = new ResultMsg();
            resultMsg.StatusCode = (int)StatusCodeEnum.Success;
            resultMsg.Msg = StatusCodeEnum.Success.GetEnumText();
            resultMsg.Data = data;
            return HttpResponseExtension.toJson(JsonConvert.SerializeObject(resultMsg));
        }
        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        protected virtual HttpResponseMessage Success(object data)
        {
            //return Content(new ResultMsg {   = ResultType.success, message = message }.ToJson());
            ResultMsg resultMsg = null;
            resultMsg = new ResultMsg();
            resultMsg.StatusCode = (int)StatusCodeEnum.Success;
            resultMsg.Msg = StatusCodeEnum.Success.GetEnumText();
            resultMsg.Data = data;
            return HttpResponseExtension.toJson(JsonConvert.SerializeObject(resultMsg));
        }
        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected virtual HttpResponseMessage Success(string message, object data)
        {
            //return Content(new ResultMsg { type = ResultType.success, message = message, resultdata = data }.ToJson());
            ResultMsg resultMsg = null;
            resultMsg = new ResultMsg();
            resultMsg.StatusCode = (int)StatusCodeEnum.Success;
            resultMsg.Msg = message ;
            resultMsg.Data = data;
            return HttpResponseExtension.toJson(JsonConvert.SerializeObject(resultMsg));
        }
     
        /// <summary>
        /// 返回失败消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        protected virtual HttpResponseMessage Error(StatusCodeEnum errorcode= StatusCodeEnum.Error)
        {
            //return Content(new ResultMsg { type = ResultType.error, message = message }.ToJson());
            ResultMsg resultMsg = null;
            resultMsg = new ResultMsg();
            resultMsg.StatusCode = (int)errorcode;
            resultMsg.Msg = errorcode.GetEnumText(); 
            resultMsg.Data = null;
            return HttpResponseExtension.toJson(JsonConvert.SerializeObject(resultMsg));
        }
    }
}
