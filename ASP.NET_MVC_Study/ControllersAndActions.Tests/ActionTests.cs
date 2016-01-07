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
    }
}
