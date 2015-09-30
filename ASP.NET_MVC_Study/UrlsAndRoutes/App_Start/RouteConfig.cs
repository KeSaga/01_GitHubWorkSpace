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
            // 创建一个既有静态也有可变元素片段的 URL 模式
            // MapRoute 将在路由集合的末尾添加一条新的路由
            // 该路由需要放在其他路由之前，原因是路由是按照他们在 RouteCollection 对象中出现的顺序被运用的。
            // 我们可以将一条路由按照指定的位置添加，但一般不采用这种方式，原因是让路由以它们被定义的顺序来
            // 运用更容易理解运用于一个应用程序的路由。
            // 因此，路由系统是先匹配最前面定义的路由，如果不能匹配，则继续下一个，所以，最好先定义较具体的路
            // 由，然后次之，以此类推。
            routes.MapRoute("", "X{controller}/{action}");

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

            // 定义静态 URL 片段（带有 Public 前缀的静态模式）
            routes.MapRoute("", "Public/{controller}/{action}", new { controller = "Home", action = "Index" });

        }
    }
}