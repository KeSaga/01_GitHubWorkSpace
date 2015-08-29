using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index_Contains_All_Producs()
        {
            // 准备——创建模仿存储库
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(
                new Product[]
                {
                    new Product{ProductID=1,Name="P1"},
                    new Product{ProductID=2,Name="P2"},
                    new Product{ProductID=3,Name="P3"}
                }.AsQueryable());

            // 准备——创建控制器
            AdminController target = new AdminController(mock.Object);

            // 动作
            Product[] result = ((IEnumerable<Product>)target.Index().ViewData.Model).ToArray();

            // 断言
            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual("P1", result[0].Name);
            Assert.AreEqual("P2", result[1].Name);
            Assert.AreEqual("P3", result[2].Name);

        }

        [TestMethod]
        public void Can_Edit_Produc()
        {
            // 准备——创建模仿存储库
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(
                new Product[]
                {
                    new Product{ProductID=1,Name="P1"},
                    new Product{ProductID=2,Name="P2"},
                    new Product{ProductID=3,Name="P3"}
                }.AsQueryable());

            // 准备——创建控制器
            AdminController target = new AdminController(mock.Object);

            // 动作
            Product p1 = target.Edit(1).ViewData.Model as Product;
            Product p2 = target.Edit(2).ViewData.Model as Product;
            Product p3 = target.Edit(3).ViewData.Model as Product;

            // 断言
            Assert.AreEqual(1, p1.ProductID);
            Assert.AreEqual(2, p2.ProductID);
            Assert.AreEqual(3, p3.ProductID);

        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_Produc()
        {
            // 准备——创建模仿存储库
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(
                new Product[]
                {
                    new Product{ProductID=1,Name="P1"},
                    new Product{ProductID=1,Name="P1"},
                    new Product{ProductID=1,Name="P1"}
                }.AsQueryable());

            // 准备——创建控制器
            AdminController target = new AdminController(mock.Object);

            // 动作
            Product result = target.Edit(4).ViewData.Model as Product;

            // 断言
            Assert.IsNull(result);

        }

        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            // 准备——创建模仿存储库
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            // 准备——创建控制器
            AdminController target = new AdminController(mock.Object);
            // 准备——创建一个产品
            Product product = new Product { Name = "Test" };

            // 动作——试着保存这个产品
            ActionResult result = target.Edit(product);

            // 断言——检查，调用了存储库
            mock.Verify(m => m.SaveProduct(product));
            // 断言——检测方法的结果类型
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));

        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            // 准备——创建模仿存储库
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            // 准备——创建控制器
            AdminController target = new AdminController(mock.Object);
            // 准备——创建一个产品
            Product product = new Product { Name = "Test" };
            // 准备——把一个错误添加到模型状态
            target.ModelState.AddModelError("error", "error");

            // 动作——试图保存产品
            ActionResult result = target.Edit(product);

            // 断言——确认存储库未被调用
            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never());
            // 断言——检测方法的结果类型
            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }

        [TestMethod]
        public void Cann_Delete_Valid_Products()
        {
            // 准备——创建一个产品
            Product prod = new Product { ProductID = 2, Name = "Test" };

            // 准备——创建模仿存储库
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product { ProductID = 1, Name = "P1" },
                prod,
                new Product { ProductID = 3, Name = "P3" }
            }.AsQueryable());

            // 准备——创建控制器
            AdminController target = new AdminController(mock.Object);

            // 动作——删除产品
            ActionResult result = target.Delete(prod.ProductID);

            // 断言——确保存储库的删除方法是针对正确的产品被调用的
            mock.Verify(m => m.DeleteProduct(prod.ProductID));

        }

    }
}
