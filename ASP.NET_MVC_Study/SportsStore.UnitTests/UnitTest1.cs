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
            //IEnumerable<Product> result = (IEnumerable<Product>)controller.List(2).Model;
            // 改用视图模型数据
            ProductsListViewModel result = (ProductsListViewModel)controller.List(null, 2).Model;

            // 断言
            Product[] prodArray = result.Products.ToArray();
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
            Assert.AreEqual(result.ToString(), @"<a href=""Page1"">1</a>"
                + @"<a class=""selected"" href=""Page2"">2</a>"
                + @"<a href=""Page3"">3</a>");

        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
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
            ProductsListViewModel result = (ProductsListViewModel)controller.List(null, 2).Model;

            // 断言
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }

        [TestMethod]
        public void Can_Filter_Products()
        {
            // 准备
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product{ProductID=1,Name="P1",Category="Cat1"},
                new Product{ProductID=2,Name="P2",Category="Cat2"},
                new Product{ProductID=3,Name="P3",Category="Cat1"},
                new Product{ProductID=4,Name="P4",Category="Cat2"},
                new Product{ProductID=5,Name="P5",Category="Cat3"}
            }.AsQueryable());

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            // 动作
            Product[] result = ((ProductsListViewModel)controller.List("Cat2", 1).Model).Products.ToArray();

            // 断言
            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0].Name == "P2" && result[0].Category == "Cat2");
            Assert.IsTrue(result[1].Name == "P4" && result[1].Category == "Cat2");
        }

        [TestMethod]
        public void Can_Create_Categories()
        {
            // 准备——创建模仿存储库
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
                {
                new Product{ProductID=1,Name="P1",Category="Apples"},
                new Product{ProductID=2,Name="P2",Category="Apples"},
                new Product{ProductID=3,Name="P3",Category="Plums"},
                new Product{ProductID=4,Name="P4",Category="Oranges"}
                }.AsQueryable());

            // 准备——创建控制器
            NavController target = new NavController(mock.Object);

            // 动作——获取分类集合
            string[] results = ((IEnumerable<string>)target.Menu().Model).ToArray();

            // 断言
            Assert.AreEqual(results.Length, 3);
            Assert.AreEqual(results[0], "Apples");
            Assert.AreEqual(results[1], "Oranges");
            Assert.AreEqual(results[2], "Plums");

        }

        [TestMethod]
        public void Indicates_Selected_Category()
        {
            // 准备——创建模仿存储库
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]{
                new Product{ProductID=1,Name="P1",Category="Apples"},
                new Product{ProductID=4,Name="P2",Category="Oranges"}
            }.AsQueryable());

            // 准备——定义控制器
            NavController target = new NavController(mock.Object);

            // 准备——定义已选分类
            string categoryToSelect = "Apples";

            // 动作
            // 注意：ViewBag 的属性值无需转换。这是 ViewBag 对象由于 ViewData 的优点之一。
            string result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

            // 断言
            Assert.AreEqual(categoryToSelect, result);

        }

        [TestMethod]
        public void Generate_Category_Specific_Product_Count()
        {
            // 准备——创建模仿存储库
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product{ProductID=1,Name="P1",Category="Cat1"},
                new Product{ProductID=2,Name="P2",Category="Cat2"},
                new Product{ProductID=3,Name="P3",Category="Cat1"},
                new Product{ProductID=4,Name="P4",Category="Cat2"},
                new Product{ProductID=5,Name="P5",Category="Cat3"}
            }.AsQueryable());

            // 准备——创建控制器并使页面容纳 3 个物品
            ProductController target = new ProductController(mock.Object);
            target.PageSize = 3;

            // 动作——测试不同分类的产品数
            int res1 = ((ProductsListViewModel)target.List("Cat1").Model).PagingInfo.TotalItems;
            int res2 = ((ProductsListViewModel)target.List("Cat2").Model).PagingInfo.TotalItems;
            int res3 = ((ProductsListViewModel)target.List("Cat3").Model).PagingInfo.TotalItems;
            int resAll = ((ProductsListViewModel)target.List(null).Model).PagingInfo.TotalItems;

            // 断言
            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }

    }
}
