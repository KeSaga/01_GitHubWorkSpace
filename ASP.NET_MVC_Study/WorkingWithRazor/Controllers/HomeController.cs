using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkingWithRazor.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            string[] names = { "Apple", "Orange", "Pear" };

            return View(names);
        }

        public ActionResult List()
        {
            return View();
        }

    }
}
