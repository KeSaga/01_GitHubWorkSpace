using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControllersAndActions.Controllers
{
    public class ExampleController : Controller
    {

        public ViewResult Index()
        {
            return View("Homepage");
        }

        ///// <summary>
        ///// 通过路径指定要渲染的视图（一般建议最好使用控制器的动作方法来定向）
        ///// </summary>
        ///// <returns></returns>
        //public ViewResult Index()
        //{
        //    return View("~/Views/Other/Index.cshtml");
        //}

        ///// <summary>
        ///// 将一个对象作为 View 方法的参数发送给视图
        ///// </summary>
        ///// <returns></returns>
        //public ViewResult Index()
        //{
        //    DateTime date = DateTime.Now;
        //    return View(date);
        //}

        ///// <summary>
        ///// 使用视图包特性：ViewBag
        ///// </summary>
        ///// <returns></returns>
        //public ViewResult Index()
        //{
        //    ViewBag.Messge = "Hello";
        //    ViewBag.Date = DateTime.Now;

        //    return View();
        //}

        /// <summary>
        /// 重定向到一个字面 URL
        /// </summary>
        /// <returns></returns>
        public RedirectResult Redirect()
        {
            // 临时重定向
            return Redirect("/Example/Index");

            //// 永久重定向
            //return RedirectPermanent("/Example/Index");
        }

    }
}
