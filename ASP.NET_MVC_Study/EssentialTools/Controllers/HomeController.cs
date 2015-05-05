using EssentialTools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EssentialTools.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        private Product[] products = { 
                                     new Product{Name="Kayak",Category="Watersports",Price=275M},
                                     new Product{Name="Lifejacket",Category="Watersports",Price =48.95M},
                                     new Product{Name="Soccer ball",Category="Soccer",Price=19.50M},
                                     new Product{Name="Corner flag",Category="Soccer",Price=34.95M}
                                     };

        public ActionResult Index()
        {
            //改用接口形式，以实现松耦合机制
            //LinqValueCalculator calc = new LinqValueCalculator();
            IValueCalculator calc = new LinqValueCalculator();

            ShoppingCart cart = new ShoppingCart(calc) { Products = products };

            decimal totalValue = cart.CalculateProductTotal();

            return View(totalValue);
        }

    }
}
