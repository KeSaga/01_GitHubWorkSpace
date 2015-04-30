using MyRazorEG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyRazorEG.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        Product myProduct = new Product
        {
            ProductID = 1,
            Name = "Kayak",
            Description = "A boat for one person",
            Category = "Watersports",
            Price = 275M
        };

        public ActionResult Index()
        {
            return View(myProduct);
        }

        //下面这个方法的视图的创建使用了布局模板，即在创建时在弹出的对话框中选择了“使用布局或母版页”这个选项，并为之指定了要使用的布局文件
        public ActionResult NameAndPrice()
        {
            return View(myProduct);
        }

        public ActionResult DemoExpression()
        {
            ViewBag.ProductCount = 1;
            ViewBag.ExpressShip = true;
            ViewBag.ApplyDiscount = false;
            ViewBag.Supplier = null;

            return View(myProduct);
        }

    }
}
