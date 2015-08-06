using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository _repository;

        public CartController(IProductRepository repo)
        {
            _repository = repo;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                //Cart = GetCart(),
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Product product = _repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                //GetCart().AddItem(product, 1);
                cart.AddItem(product, 1);
            }
            // ------------注意：------------------
            // 此方法下面和 RemoveFromCart 方法中使用的 RedirectToAction 方法的效果是
            // 把一个 HTTP 的重定向指令发送到客户端浏览器，要求浏览器请求一个新的 URL。
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = _repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                //GetCart().RemoveLine(product);
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        #region -------------------------使用模型绑定器------------------------

        // 改用使用模型绑定器来实现 Cart 对象的获取（一个实现了 IModelBinder 接口的自定义模型绑定器：CartModelBinder）
        // 使用自定义模型绑定器的优点：
        // 1、可以把用来创建对象（数据模型类）与创建控制器的逻辑分离
        // 2、任何使用对象的控制器类都能简单的把这些对象声明为动作方法参数，并能够利用自定义模型绑定器
        // 3、能对该对象的控制器进行单元测试，而不需要模仿大量的 ASP.NET 通道

        //private Cart GetCart()
        //{
        //    // 运用 ASP.NET 会话（Session）状态特性来存储和接收 Cart 对象
        //    // Session 状态对象默认存储在 ASP.NET 服务器的内存中，但可以配置不同的存储方式，包括使用一个 SQL 数据库。
        //    Cart cart = (Cart)Session["Cart"];
        //    if (cart == null)
        //    {
        //        cart = new Cart();
        //        Session["Cart"] = cart;
        //    }
        //    return cart;
        //}

        #endregion End -------------------------使用模型绑定器------------------------

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

    }
}
