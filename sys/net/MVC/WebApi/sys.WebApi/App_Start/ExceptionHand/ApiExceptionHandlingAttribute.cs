using Newtonsoft.Json;
using sys.WebApi.Common;
using sys.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace sys.WebApi
{
    /// <summary>
    /// API自定义错误过滤器属性 
    /// 用法 action头叫上  [ApiExceptionHandlingAttribute]
    /// </summary>
    public class ApiExceptionHandlingAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// 统一对调用异常信息进行处理，返回自定义的异常信息
        /// </summary>
        /// <param name="context">HTTP上下文对象</param>
        public override void OnException(HttpActionExecutedContext context)
        {

            ResultMsg resultMsg = null;
            resultMsg = new ResultMsg();
            //自定义异常的处理
            if (context.Exception is NotImplementedException)
            {
                resultMsg.StatusCode = (int)StatusCodeEnum.NotImplemented;
                resultMsg.Msg = StatusCodeEnum.NotImplemented.GetEnumText();
                resultMsg.Data = null;
                //HttpResponseExtension.toJson(JsonConvert.SerializeObject(resultMsg));
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotImplemented)
                {
                    //封装处理异常信息，返回指定JSON对象
                    Content = HttpResponseExtension.toStringContentJson(JsonConvert.SerializeObject(resultMsg)),
                    //Content = new StringContent(JsonHelper.ToJson(new ErrorModel((int)HttpStatusCode.NotImplemented, 0, ex.Message)), Encoding.UTF8, "application/json"),
                    ReasonPhrase = "NotImplementedException"
                });
            }
            else if (context.Exception is TimeoutException)
            {
                resultMsg.StatusCode = (int)StatusCodeEnum.RequestTimeout;
                resultMsg.Msg = StatusCodeEnum.RequestTimeout.GetEnumText();
                resultMsg.Data = null;
                //HttpResponseExtension.toJson(JsonConvert.SerializeObject(resultMsg));
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.RequestTimeout)
                {
                    //封装处理异常信息，返回指定JSON对象
                    //Content = new StringContent(JsonHelper.ToJson(new ErrorModel((int)HttpStatusCode.RequestTimeout, 0, ex.Message)), Encoding.UTF8, "application/json"),
                    Content = HttpResponseExtension.toStringContentJson(JsonConvert.SerializeObject(resultMsg)),
                    ReasonPhrase = "TimeoutException"
                });
            }
            //.....这里可以根据项目需要返回到客户端特定的状态码。如果找不到相应的异常，统一返回服务端错误500
            else
            {
                resultMsg.StatusCode = (int)StatusCodeEnum.Error;
                resultMsg.Msg = StatusCodeEnum.Error.GetEnumText();
                resultMsg.Data = null;
                //HttpResponseExtension.toJson(JsonConvert.SerializeObject(resultMsg));
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    //封装处理异常信息，返回指定JSON对象
                    //Content = new StringContent(JsonHelper.ToJson(new ErrorModel((int)HttpStatusCode.InternalServerError, 0, ex.Message)), Encoding.UTF8, "application/json"),
                    Content = HttpResponseExtension.toStringContentJson(JsonConvert.SerializeObject(resultMsg)),
                    ReasonPhrase = "InternalServerErrorException"
                });
            }

            //base.OnException(context);

            //记录关键的异常信息
            //Debug.WriteLine(context.Exception);
        }
    }

}