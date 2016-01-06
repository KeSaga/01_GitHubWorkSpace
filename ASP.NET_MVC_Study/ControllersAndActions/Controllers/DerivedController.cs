using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ControllersAndActions.Infrastructure;

namespace ControllersAndActions.Controllers
{
    public class DerivedController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Hello from the DerivedController Index method";
            return View("MyView");
        }

        public void ProduceOutput()
        {
            if (Server.MachineName == "TINY")
            {
                Response.Redirect("/Derived/Index");
            }
            else
            {
                Response.Write("Controller: Derived, Action: ProduceOutput");
            }
        }

        ///// <summary>
        ///// 演示对 CustomRedirectResult 对象调用的使用方法
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult ProduceOutput()
        //{
        //    //// 使用内建的 RedirectResult 对象
        //    //return new RedirectResult("/Basic/Index");
        //    if (Server.MachineName == "TINY")
        //    {
        //        return new CustomRedirectResult { Url="/Basic/Index"};
        //    }
        //    else
        //    {
        //        Response.Write("Controller: Derived, Action: ProduceOutput");
        //        return null;
        //    }
        //}

    }
}
