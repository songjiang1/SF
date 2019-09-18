using Newtonsoft.Json; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace sys.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            // 注册全局异常返回格式统一 
            config.MessageHandlers.Add(new CustomErrorMessageDelegatingHandler());
            // Web API 路由
            config.MapHttpAttributeRoutes();


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new {
                    id = RouteParameter.Optional,
                    namespace_name = new string[] { "sys.WebApi" }
                    }
            );
            var jsonFormatter = new JsonMediaTypeFormatter();
            var settings = jsonFormatter.SerializerSettings;
            settings.NullValueHandling = NullValueHandling.Ignore;//解决序列化出来的JSON NULL 值处理
            //注册返回json的ContentNegotiator，替换默认的DefaultContentNegotiator
            config.Services.Replace(typeof(IContentNegotiator), new JsonContentNegotiator(jsonFormatter));

             

            //注册支持namespace的HttpControllerSelector，替换默认DefaultHttpControllerSelector
            //config.Services.Replace(typeof(IHttpControllerSelector), new NamespaceHttpControllerSelector(GlobalConfiguration.Configuration));
 

             

        }
    }
}
