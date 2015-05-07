using EssentialTools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//使用Ninject
using Ninject;

namespace EssentialTools.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        private Product[] _products = { 
                                     new Product{Name="Kayak",Category="Watersports",Price=275M},
                                     new Product{Name="Lifejacket",Category="Watersports",Price =48.95M},
                                     new Product{Name="Soccer ball",Category="Soccer",Price=19.50M},
                                     new Product{Name="Corner flag",Category="Soccer",Price=34.95M}
                                     };

        #region 添加控制器的构造函数

        // 通过创建全局私有的接口对象，并通过添加的控制器构造函数的参数
        // （该接口对象的接口类型参数）注入具体的实现来进行赋值的方式来
        // 进行对 HomeController 对该接口的具体实现的解耦

        // ----------------------------------基本工作流程-----------------------------------------------
        // 1、浏览器项 MVC 发送一个请求 Home 的 URL，MVC 框架猜出该请求意指 Home 控制器，于是
        //    会创建 HomeController 类实例
        // 2、MVC 框架在创建 HomeController 类实例的过程中会发现其构造器有一个对 IValueCalculator
        //    接口的依赖项，便会要求依赖性解析器对此依赖项进行解析，将该接口指定为依赖性解析器中的
        //    GetService 方法所使用的类型参数
        // 3、依赖性解析器将传递来的类型参数交给 TryGet 方法，要求 Ninject 创建一个新的 IValueCalculator
        //    接口类实例
        // 4、Ninject 会检测到该接口与其实现类 LinqValueCalculator 具有绑定关系，于是将为该接口创建
        //    一个 LinqValueCalculator 类实例，并返回给依赖性解析器
        // 5、依赖性解析器将 Ninject 返回的 LinqValueCalculator 类作为 IValueCalculator 接口实现类
        //    实例回递给 MVC 框架
        // 6、MVC 框架礼仪依赖性解析器返回的接口类实例创建 HomeController 控制器实例，并使用该控制器
        //    实例队请求进行服务
        // --------------------------------------------------------------------------------------------

        private IValueCalculator _calc;

        public HomeController(IValueCalculator calcParam)
        {
            _calc = calcParam;
        }

        #endregion

        public ActionResult Index()
        {
            #region 使用借款形式以实现松耦合机制

            // 改用接口形式，以实现松耦合机制，缺点是不能解除 HomeController 对
            // LinqValueCalculator 的依赖性（即这两者之间仍是紧耦合）
            ////LinqValueCalculator calc = new LinqValueCalculator();
            //IValueCalculator calc = new LinqValueCalculator();

            #endregion End 使用借款形式以实现松耦合机制

            #region 使用 Ninject （DI容器）

            // 使用 Ninject （DI容器）
            // 注意：下面这三个步骤只是简单说明 Ninject 的基本使用方法，
            // 在这里使用这种方式还不能解决当前控制器：HomeController 对
            // LinqValueCalculator 的依赖性（即这两者之间仍是紧耦合）
            //IKernel ninjectKernel = new StandardKernel();
            //ninjectKernel.Bind<IValueCalculator>().To<LinqValueCalculator>();

            //IValueCalculator calc = ninjectKernel.Get<IValueCalculator>();

            #endregion End 使用 Ninject （DI容器）

            ShoppingCart cart = new ShoppingCart(_calc) { Products = _products };

            decimal totalValue = cart.CalculateProductTotal();

            return View(totalValue);
        }

    }
}
