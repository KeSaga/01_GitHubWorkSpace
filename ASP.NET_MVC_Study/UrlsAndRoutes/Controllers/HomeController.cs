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

        /// <summary>
        /// 用自定义变量作为动作方法参数
        /// </summary>
        /// <param name="id">
        /// MVC 框架会尝试将 URL 的值转换成所定义的参数类型，这
        /// 里将转换成 string ，MVC 框的这以特性将方便开发者不必
        /// 自行做转换。
        /// </param>
        /// <returns></returns>
        //public ActionResult CustomVariable(string id)
        //{
        //    ViewBag.Controller = "Home";
        //    ViewBag.Action = "CustomVariable";
        //    ViewBag.CustomVariable = id;
        //    return View();
        //}


        //public ActionResult CustomVariable(string id)
        //{
        //    ViewBag.Controller = "Home";
        //    ViewBag.Action = "CustomVariable";
        //    // 检查是否为一个可选片段变量提供了值
        //    ViewBag.CustomVariable = id == null ? "<no value>" : id;
        //    return View();
        //}

        /// <summary>
        /// 使用 C# 的可选参数为动作方法参数定义默认值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public ActionResult CustomVariable(string id = "DefaultId")
        //{
        //    ViewBag.Controller = "Home";
        //    ViewBag.Action = "CustomVariable";
        //    ViewBag.CustomVariable = id;
        //    return View();
        //}

    }
}
