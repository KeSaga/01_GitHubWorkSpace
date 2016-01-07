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

    }
}
