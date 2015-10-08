using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Web.Routing;
using Moq;
using System.Reflection;

namespace UrlsAndRoutes.Tests
{
    [TestClass]
    public class RouteTests
    {
        /// <summary>
        /// 辅助器方法：创建了模仿的 HttpContextBase 对象
        /// </summary>
        /// <param name="targetUrl"></param>
        /// <param name="httpMethod"></param>
        /// <returns></returns>
        private HttpContextBase CreateHttpContext(string targetUrl = null, string httpMethod = "GET")
        {
            // 创建模仿请求
            Mock<HttpRequestBase> mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(m => m.AppRelativeCurrentExecutionFilePath).Returns(targetUrl);
            mockRequest.Setup(m => m.HttpMethod).Returns(httpMethod);

            // 创建模仿响应
            Mock<HttpResponseBase> mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(m => m.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(s => s);

            // 创建使用上述请求和响应的模仿上下文
            Mock<HttpContextBase> mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(m => m.Request).Returns(mockRequest.Object);
            mockContext.Setup(m => m.Response).Returns(mockResponse.Object);

            // 返回模仿的上下文
            return mockContext.Object;

        }

        /// <summary>
        /// 辅助器方法：让用户能够测试一条路由
        /// </summary>
        /// <param name="url"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <param name="routeProperties"></param>
        /// <param name="httpMethod"></param>
        private void TestRouteMatch(string url, string controller, string action, object routeProperties = null, string httpMethod = "GET")
        {
            // 准备
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // 动作——处理路由
            RouteData result = routes.GetRouteData(CreateHttpContext(url, httpMethod));

            // 断言
            Assert.IsNotNull(result);
            Assert.IsTrue(TestIncomingRouteResult(result, controller, action, routeProperties));

        }

        /// <summary>
        /// 辅助器方法：比较路由系统获得的结果与片段变量期望的值
        /// </summary>
        /// <param name="routeResult"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <param name="propertySet"></param>
        /// <returns></returns>
        private bool TestIncomingRouteResult(RouteData routeResult, string controller, string action, object propertySet = null)
        {
            Func<object, object, bool> valCompare = (v1, v2) => { return StringComparer.InvariantCultureIgnoreCase.Compare(v1, v2) == 0; };

            bool result = valCompare(routeResult.Values["controller"], controller)
                && valCompare(routeResult.Values["action"], action);

            if (propertySet != null)
            {
                PropertyInfo[] propInfo = propertySet.GetType().GetProperties();
                foreach (PropertyInfo pi in propInfo)
                    if (!(routeResult.Values.ContainsKey(pi.Name)
                        && valCompare(routeResult.Values[pi.Name], pi.GetValue(propertySet, null))
                        ))
                    {
                        result = false;
                        break;
                    }
            }
            return result;
        }

        /// <summary>
        /// 辅助器方法：检测一个 URL 不工作的情况
        /// </summary>
        /// <param name="url"></param>
        private void TestRouteFail(string url)
        {
            // 准备
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            // 动作——处理路由
            RouteData result = routes.GetRouteData(CreateHttpContext(url));
            // 断言
            Assert.IsTrue(result == null || result.Route == null);

        }

        [TestMethod]
        public void TestIncomingRoutes()
        {
            //// 对用户希望接收的 URL 进行检测
            //TestRouteMatch("~/Admin/Index","Admin","Index");
            //// 检查通过片段获取的值
            //TestRouteMatch("~/One/Two", "One", "Two");

            //// 确保太多或太少的片段书不会匹配
            //TestRouteFail("~/Admin/Index/Segment");
            //TestRouteFail("~/Admin");

            // ----注：在测试时必须有波浪线(~)字符作为 URL 的前缀，因为这是 ASP.NET 框架把 URL 表现给路由系统的方式。----


            TestRouteMatch("~/", "Home", "Index");
            TestRouteMatch("~/Customer", "Customer", "Index");
            TestRouteMatch("~/Shop/Index", "Home", "Index");
            TestRouteMatch("~/Customer/List", "Customer", "List");
            //TestRouteFail("~/Customer/List/All");

            // 测试自定义片段变量----不做演示，因此不必须要该功能
            //TestRouteMatch("~/", "Home", "Index", new { id = "DefaultId" });
            //TestRouteMatch("~/Customer", "Customer", "Index", new { id = "DefaultId" });
            //TestRouteMatch("~/Customer/List", "Customer", "List", new { id = "DefaultId" });
            //TestRouteMatch("~/Customer/List/All", "Customer", "List", new { id = "DefaultId" });
            //TestRouteFail("~/Customer/List/All/Delete");

        }

    }
}
