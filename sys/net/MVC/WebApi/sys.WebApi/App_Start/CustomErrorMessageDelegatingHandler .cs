using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using sys.WebApi.Common;
using sys.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace sys.WebApi
{
    /// <summary>
    /// 消息代理处理，用来捕获这些特殊的异常信息
    /// </summary> 

    //public class CustomErrorMessageDelegatingHandler : DelegatingHandler
    //{
    //    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    //    {
    //        return base.SendAsync(request, cancellationToken).ContinueWith<HttpResponseMessage>((responseToCompleteTask) =>
    //        {
    //            HttpResponseMessage response = responseToCompleteTask.Result;
    //            HttpError error = null;
    //            if (response.TryGetContentValue<HttpError>(out error))
    //            {
    //                //自定义错误处理
    //                //error.Message = "这个接口调用出错了";
    //            }
    //            if (error != null)
    //            {   //这是本人创建的一个返回类                 
    //                var resultMsg = new ResultMsg { StatusCode = (int)StatusCodeEnum.URLExpireError, Msg = error.MessageDetail };
    //                return new HttpResponseMessage
    //                {
    //                    Content = new StringContent(resultMsg.ToJson(),
    //                    System.Text.Encoding.GetEncoding("UTF-8"), "application/json"),
    //                    StatusCode = HttpStatusCode.OK
    //                };
    //            }
    //            else
    //            {
    //                return response;
    //            }
    //        });
    //    }


    //}
    /// <summary>
    /// API自定义错误消息处理委托类。
    /// 用于处理访问不到对应API地址的情况，对错误进行自定义操作。
    /// </summary>
    public class CustomErrorMessageDelegatingHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken).ContinueWith<HttpResponseMessage>((responseToCompleteTask) =>
            {
                HttpResponseMessage response = responseToCompleteTask.Result;
                HttpError error = null;
                if (response.TryGetContentValue<HttpError>(out error))
                {
                    //添加自定义错误处理
                    //error.Message = "Your Customized Error Message";
                }

                if (error != null)
                {
                    //获取抛出自定义异常，有拦截器统一解析
                    ResultMsg resultMsg = new ResultMsg();
                    resultMsg.StatusCode = (int)StatusCodeEnum.NotFound;
                    resultMsg.Msg = StatusCodeEnum.NotFound.GetEnumText();
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        //封装处理异常信息，返回指定JSON对象
                        //Content = new StringContent(JsonHelper.ToJson(new ErrorModel(404, 0, error.Message)), Encoding.UTF8, "application/json"),
                        Content = HttpResponseExtension.toStringContentJson(JsonConvert.SerializeObject(resultMsg)), 
                        ReasonPhrase = "Exception"
                    }); 
                }
                else
                {
                    return response;
                }
            });
        }
    }
}