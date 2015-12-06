﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UrlsAndRoutes.Infrastructure;

namespace UrlsAndRoutes
{
    public class RouteConfig
    {
        //public static void RegisterRoutes(RouteCollection routes)
        //{
        //    // 为控制器和动作方法创建一个别名
        //    routes.MapRoute("ShopSchema2", "Shop/OldAction", new { controller = "Home", action = "Index" });
        //    // 结合静态片段和默认值为特定的路由创建一个别名
        //    // 匹配第一个片段是 Shop 的任意两片段 URL，action 的值取自第二个 URL 片段。
        //    // 由于此 URL 模式未提供 controler 的可变片段，所以会使用提供的默认值（“Home”）。即对
        //    // Shop 控制器上的一个动作的请求会被转换成对 Home 控制器的请求。
        //    routes.MapRoute("ShopSchema", "Shop/{action}", new { controller = "Home" });

        //    // 创建一个既有静态也有可变元素片段的 URL 模式
        //    // MapRoute 将在路由集合的末尾添加一条新的路由
        //    // 该路由需要放在其他路由之前，原因是路由是按照他们在 RouteCollection 对象中出现的顺序被运用的。
        //    // 我们可以将一条路由按照指定的位置添加，但一般不采用这种方式，原因是让路由以它们被定义的顺序来
        //    // 运用更容易理解运用于一个应用程序的路由。
        //    // 因此，路由系统是先匹配最前面定义的路由，如果不能匹配，则继续下一个，所以，最好先定义较具体的路
        //    // 由，然后次之，以此类推。
        //    routes.MapRoute("", "X{controller}/{action}");

        //    routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

        //    routes.MapRoute(
        //        name: "Default",
        //        url: "{controller}/{action}/{id}",
        //        defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
        //    );

        //    // 通过创建一个新的 Route 实现路由的注册
        //    //Route myRoute = new Route("{controller}/{action}", new MvcRouteHandler());
        //    //routes.Add("MyRoute", myRoute);

        //    // 使用 RouteCollection 类所定义的MapRoute方法实现路由的注册（效果与上面方式相同）
        //    // 下面第三个参数提供了一个包含默认路由的值的对象，当 URL 片段无匹配的值时（如片段少于定义给定的片段数时），
        //    // 便会使用默认值（片段数需要符合定义的路由片段数，太多时将不做匹配）。
        //    routes.MapRoute("MyRoute", "{controller}/{action}", new { controller = "Home", action = "Index" });

        //    // 定义静态 URL 片段（带有 Public 前缀的静态模式）
        //    routes.MapRoute("", "Public/{controller}/{action}", new { controller = "Home", action = "Index" });

        //    // 定义一个名为“id”的自定义变量。如果没有与之对应的片段内容，则将使用默认值
        //    //routes.MapRoute("MyRoute", "{controller}/{action}/{id}",
        //    //    new { controller = "Home", action = "Index", id = "DefaultId" });

        //    // 定义可选 URL 片段。该路由的效果是：无论是否提供id，都将进行匹配
        //    //routes.MapRoute("MyRoute", "{controller}/{action}/{id}",
        //    //    new { controller = "Home", action = "Index", id = UrlParameter.Optional });

        //    //// 定义可变长路由
        //    //routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}",
        //    //    new { controller = "Home", action = "Index", id = UrlParameter.Optional });

        //}

        public static void RegisterRoutes(RouteCollection routes)
        {
            //// 指定命名空间解析顺序，MVC 将在处理其他命名空间之前优先处理 UrlsAndRoutes.AdditionalControllers 命名空间
            //routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}",
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional }, new string[] { "UrlsAndRoutes.AdditionalControllers" });

            //// 如果需要对一个命名空间中某个控制器给予优先，但又要解析另一命名空间中的其他控制器，则需要创建多条路由
            //routes.MapRoute("AddControllerRoute", "{controller}/{action}/{id}/{*catchall}",
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    new string[] { "UrlsAndRoutes.AdditionalControllers" });
            //routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}",
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    new string[] { "UrlsAndRoutes.Controllers" });

            // 禁用备用命名空间
            Route myRoute = routes.MapRoute("AddControllerRoute", "{controller}/{action}/{id}/{*catchall}",
                  new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                  new string[] { "UrlsAndRoutes.AdditionalControllers" });
            myRoute.DataTokens["UseNamespaceFallback"] = false;

        }

        ///// <summary>
        ///// 使用正则表达式约束一条路由
        ///// </summary>
        ///// <param name="routes"></param>
        //public static void RegisterRoutes(RouteCollection routes)
        //{
        //    routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}",
        //          new { controller = "Home", action = "Index", id = UrlParameter.Optional },
        //          new { controller = "^H.*" },
        //          new string[] { "UrlsAndRoutes.Controllers" });

        //}

        ///// <summary>
        ///// 将一条路由约束到一组指定的值
        ///// </summary>
        ///// <param name="routes"></param>
        //public static void RegisterRoutes(RouteCollection routes)
        //{
        //    routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}",
        //          new { controller = "Home", action = "Index", id = UrlParameter.Optional },
        //          new { controller = "^H.*", action = "^Index$|^About&=$" },
        //          new string[] { "UrlsAndRoutes.Controllers" });

        //}

        ///// <summary>
        ///// 基于 HTTP 方法进行路由的约束
        ///// </summary>
        ///// <param name="routes"></param>
        //public static void RegisterRoutes(RouteCollection routes)
        //{
        //    routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}",
        //          new { controller = "Home", action = "Index", id = UrlParameter.Optional },
        //          new { controller = "^H.*", action = "Index|About",
        //              httpMethod = new HttpMethodConstraint("GET") },
        //          new string[] { "UrlsAndRoutes.Controllers" });
        //}

        ///// <summary>
        ///// 自定义路由约束的使用
        ///// </summary>
        ///// <param name="routes"></param>
        //public static void RegisterRoutes(RouteCollection routes)
        //{
        //    // 下面这句话的作用是启用文件检查前的路由评估
        //    routes.RouteExistingFiles = true;

        //    // 使用 IgnoreRoute 方法
        //    routes.IgnoreRoute("Content/{filename}.html");

        //    routes.MapRoute("DiskFile", "Content/StaticContent.html",
        //        new { controller = "Customer", action = "List" });

        //    routes.MapRoute("ChromeRoute", "{*catchall}",
        //          new { controller = "Home", action = "Index" },
        //          new
        //          {
        //              customConstraint = new UserAgentConstratint("Chrome")
        //          },
        //          new string[] { "UrlsAndRoutes.AdditionalControllers" });

        //    routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}",
        //          new { controller = "Home", action = "Index", id = UrlParameter.Optional },
        //          new string[] { "UrlsAndRoutes.Controllers" });
        //}

        ///// <summary>
        ///// 为演示用路由系统生成输出 URL 而设计的路由方案
        ///// </summary>
        ///// <param name="routes"></param>
        //public static void RegisterRoutes(RouteCollection routes)
        //{
        //    routes.MapRoute("NewRoute", "App/Do{action}",
        //        new { controller = "Home" });
        //    routes.MapRoute("MyRoute", "{controller}/{action}/{id}",
        //        new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        //}

        ///// <summary>
        ///// 为了配合使用指定路由生成ULR而做的修改
        ///// </summary>
        ///// <param name="routes"></param>
        //public static void RegisterRoutes(RouteCollection routes)
        //{
        //    routes.MapRoute("MyRoute", "{controller}/{action}");
        //    routes.MapRoute("MyOtherRoute", "App/{action}", new { controller = "Home" });

        //}

    }
}