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
            //// 读取重定向至该方法之前设置的临时数据的值
            //ViewBag.Message = TempData["Message"];
            //ViewBag.Date = TempData["Date"];

            //// 使用 Peek 方法实现读取 TempData 中的值但不将其标记为删除的方式
            //DateTime time = (DateTime)TempData.Peek("Date");

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

        /// <summary>
        /// 重定向到一个路由系统的 URL
        /// </summary>
        /// <returns></returns>
        public RedirectToRouteResult RedirectToRoute()
        {
            // 重定向到一个路由系统的 URL
            return RedirectToRoute(new
            {
                controller = "Example",
                action = "Index",
                ID = "MyID"
            });
        }

        /// <summary>
        /// 用 RedirectToAction 方法重定向
        /// </summary>
        /// <returns></returns>
        public RedirectToRouteResult RedirectToAction()
        {
            // 使用 TempData 保存重定向数据，以实现跨请求的重定向情况下控制器与视图之间的数据传递
            TempData["Message"] = "Hello";
            TempData["Date"] = DateTime.Now;
            // 重定向到当前控制器的动作方法
            return RedirectToAction("Index");

            //// 重定向到指定控制器（另一个控制器）的动作方法
            //return RedirectToAction("Index", "Basic");
        }

        /// <summary>
        /// 发送一个指定的状态码（此例返回一个 404 代码）
        /// </summary>
        /// <returns></returns>
        public HttpStatusCodeResult StatusCode()
        {
            return new HttpStatusCodeResult(404, "URL cannot be serviced");
        }

        /// <summary>
        /// 使用 HttpNotFound 方法返回 404 代码
        /// </summary>
        /// <returns></returns>
        public HttpStatusCodeResult StatusCode404()
        {
            return HttpNotFound();
        }

        /// <summary>
        /// 返回 401 代码
        /// </summary>
        /// <returns></returns>
        public HttpStatusCodeResult StatusCode401()
        {
            return new HttpUnauthorizedResult();
        }

    }
}
