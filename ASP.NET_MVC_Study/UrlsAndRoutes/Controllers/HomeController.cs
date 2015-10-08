using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UrlsAndRoutes.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Controller = "Home";
            ViewBag.Action = "Index";
            return View("ActionName");
        }

        /// <summary>
        /// 获取 URL 模式中自定义变量（“id”）的值，并用 ViewBag 将它传递给视图。
        /// </summary>
        /// <returns></returns>
        //public ActionResult CustomVariable()
        //{
        //    ViewBag.Controller = "Home";
        //    ViewBag.Action = "CustomVariable";
        //    ViewBag.CustomVariable = RouteData.Values["id"];
        //    return View();
        //}

    }
}
