using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SportsStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            // 路由将会按照被设定的顺序进行处理，因此下面这条路由设置将优先于后面的那条
            routes.MapRoute(
                null,
                "",
                new { controller = "Product", action = "List", category = (string)null, page = 1 }
                );

            routes.MapRoute(
                null,
                "Page{page}",
                new { controller = "Product", action = "List", category = (string)null },
                new { page = @"\d+" }
                );

            routes.MapRoute(
                null,
                "{category}",
                new { controller = "Product", action = "List", page = 1 }
                );

            routes.MapRoute(
                null,
                "{category}/Page{page}",
                new { controller = "Product", action = "List" },
                new { page = @"\d+" }
                );

            routes.MapRoute(null, "{controller}/{action}");

            //// 修改下面的 Home 和 Index，将其分别改为要指定的默认位置，如该示例的：
            //// *** 注：其中控制器 Product 不能写成 ProductController 的形式，因为 ProductController 是类名，控制器 ***
            //// *** 要求以 Controller 结尾，但在对该控制器类引用时需要忽略类名中的 Controller 部分                  ***
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Product", action = "List", id = UrlParameter.Optional }
            //);
        }
    }
}