using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models;
using SportsStore.WebUI.HtmlHelpers;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            // 准备
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product{ProductID=1,Name="P1"},
                new Product{ProductID=2,Name="P2"},
                new Product{ProductID=3,Name="P3"},
                new Product{ProductID=4,Name="P4"},
                new Product{ProductID=5,Name="P5"}
            }.AsQueryable());

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            // 动作
            IEnumerable<Product> result = (IEnumerable<Product>)controller.List(2).Model;

            // 断言
            Product[] prodArray = result.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");

        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            // 准备——定义一个 HTML 辅助器，为了运用扩展方法，需要这样
            HtmlHelper myHelper = null;

            // 准备——创建 PagingInfo 数据
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            //准备——用 lambda 表达试建立委托
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // 动作
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // 断言
            Assert.AreEqual(result.ToString(),@"<a href=""Page1"">1</a>"
                +@"<a class=""selected"" href=""Page2"">2</a>"
                +@"<a href=""Page3"">3</a>");

        }

    }
}
