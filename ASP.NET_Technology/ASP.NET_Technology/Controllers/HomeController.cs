using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP.NET_Technology.Models;

namespace ASP.NET_Technology.Controllers
{
    public class HomeController : Controller
    {

        public JsonResult Index()
        {
            LyfItemVM itmVM = new LyfItemVM
                {
                    ItemName = "Saga"
                };

            return Json(itmVM);
        }

        public ViewResult TestView()
        {
            return View();
        }

    }
}
