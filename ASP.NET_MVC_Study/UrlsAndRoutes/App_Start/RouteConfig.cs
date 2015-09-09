using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UrlsAndRoutes
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            // 通过创建一个新的 Route 实现路由的注册
            //Route myRoute = new Route("{controller}/{action}", new MvcRouteHandler());
            //routes.Add("MyRoute", myRoute);

            // 使用 RouteCollection 类所定义的MapRoute方法实现路由的注册（效果与上面方式相同）
            // 下面第三个参数提供了一个包含默认路由的值的对象，当 URL 片段无匹配的值时（如片段少于定义给定的片段数时），
            // 便会使用默认值（片段数需要符合定义的路由片段数，太多时将不做匹配）。
            routes.MapRoute("MyRoute", "{controller}/{action}", new { controller = "Home", action = "Index" });

        }
    }
}