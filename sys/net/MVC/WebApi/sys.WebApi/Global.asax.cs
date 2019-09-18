using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;

namespace sys.WebApi
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码 

            //注册ASP.NET MVC应用程序中的所有区域。
            AreaRegistration.RegisterAllAreas();

            //配置WebApi
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //注册过滤器
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters); 
            //注册路由配置
            RouteConfig.RegisterRoutes(RouteTable.Routes); 
        }
    }
}