using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ControllersAndActions.Controllers;
using System.Web.Mvc;

namespace ControllersAndActions.Tests
{
    [TestClass]
    public class ActionTests
    {
        [TestMethod]
        public void ViewSelectionTest()
        {
            // 准备——创建控制器
            ExampleController target = new ExampleController();
            // 动作——调用动作方法
            ViewResult result = target.Index();
            // 断言——检测结果
            Assert.AreEqual("Homepage", result.ViewName);
        }

        ///// <summary>
        ///// 通过 ViewResult.ViewData.Model 属性访问从动作方法传递给视图的视图模型对象。
        ///// </summary>
        //[TestMethod]
        //public void ViewSelectionTest()
        //{
        //    // 该测试方法测试的是下面示例的动作方法
        //    //public ViewResult Index()
        //    //{
        //    //    return View((object)"Hello,World");
        //    //}

        //    // 准备——创建控制器
        //    ExampleController target = new ExampleController();

        //    // 动作——调用动作方法
        //    ViewResult result = target.Index();

        //    // 断言——检查结果
        //    Assert.AreEqual("Hello,World", result.ViewData.Model);

        //}

        ///// <summary>
        ///// 通过 ViewResult.ViewBag 来读取 ViewBag，并对其进行测试
        ///// </summary>
        //[TestMethod]
        //public void ViewSelectionTest()
        //{
        //    // 准备——创建控制器
        //    ExampleController target = new ExampleController();

        //    // 动作——调用动作方法
        //    ViewResult result = target.Index();

        //    // 断言——检查结果
        //    Assert.AreEqual("Hello", result.ViewBag.Message);

        //}

        /// <summary>
        /// 测试：字面重定向
        /// </summary>
        [TestMethod]
        public void RedirectTest()
        {
            // 准备——创建控制器
            ExampleController target = new ExampleController();

            // 动作——调用动作方法
            RedirectResult result = target.Redirect();

            // 断言——检查结果
            Assert.IsFalse(result.Permanent);
            Assert.AreEqual("/Example/Index", result.Url);

        }

        /// <summary>
        /// 测试： 路由重定向
        /// </summary>
        [TestMethod]
        public void RedirectValueTest()
        {
            // 准备——创建控制器
            ExampleController target = new ExampleController();

            // 动作——调用动作方法
            RedirectToRouteResult result = target.RedirectToRoute();

            // 断言——检查结果
            Assert.IsFalse(result.Permanent);
            Assert.AreEqual("Example", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("MyID", result.RouteValues["ID"]);

        }

        /// <summary>
        /// 测试： HTTP 状态码
        /// </summary>
        [TestMethod]
        public void StatusCodeResultTest()
        {
            // 准备——创建控制器
            ExampleController target = new ExampleController();

            // 动作——调用动作方法
            HttpStatusCodeResult result = target.StatusCode();

            // 断言——检查结果
            Assert.AreEqual(404, result.StatusCode);

        }

    }
}
