using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void Can_Add_New_Lines()
        {
            // 准备——创建一些测试产品
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };

            // 准备——创建一个新的购物车
            Cart target = new Cart();

            // 动作
            target.AddItem(p1, 1);
            target.AddItem(p2, 2);
            CartLine[] results = target.Lines.ToArray();

            // 断言
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Product, p1);
            Assert.AreEqual(results[1].Product, p2);

        }

        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            // 准备——创建一些测试产品
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };

            // 准备——创建一个新的购物车
            Cart target = new Cart();

            // 动作
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);
            CartLine[] results = target.Lines.OrderBy(c => c.Product.ProductID).ToArray();

            // 断言
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Quantity, 11);
            Assert.AreEqual(results[1].Quantity, 1);

        }

        [TestMethod]
        public void Can_Remove_Line()
        {
            // 准备——创建一些测试产品
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Product p3 = new Product { ProductID = 3, Name = "P3" };

            // 准备——创建一个新的购物车
            Cart target = new Cart();

            // 准备——为购物车添加一些产品
            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);

            // 动作
            target.RemoveLine(p2);

            // 断言
            Assert.AreEqual(target.Lines.Where(c => c.Product == p2).Count(), 0);
            Assert.AreEqual(target.Lines.Count(), 2);

        }

        [TestMethod]
        public void Calculate_Cart_Total()
        {
            // 准备——创建一些测试产品
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };

            // 准备——创建一个新的购物车
            Cart target = new Cart();

            // 动作
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);
            decimal result = target.ComputeTotalValue();


            // 断言
            Assert.AreEqual(result, 450M);

        }

        [TestMethod]
        public void Can_Clear_Contents()
        {
            // 准备——创建一些测试产品
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };

            // 准备——创建一个新的购物车
            Cart target = new Cart();

            // 准备——为购物车添加一些产品
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);

            // 动作
            target.Clear();

            // 断言
            Assert.AreEqual(target.Lines.Count(), 0);

        }

        [TestMethod]
        public void Can_Add_To_Cart()
        {
            // 准备——创建模仿存储库
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] 
            {
                new Product { ProductID = 1, Name = "P1", Category = "Apples" } 
            }.AsQueryable());

            // 准备——创建 Cart
            Cart cart = new Cart();

            // 准备——创建控制器
            CartController target = new CartController(mock.Object, null);

            // 动作——对 Cart 添加一个产品
            target.AddToCart(cart, 1, null);

            // 断言
            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToArray()[0].Product.ProductID, 1);

        }

        [TestMethod]
        public void Adding_Product_To_Cart_Goes_To_Cart_Screen()
        {
            // 准备——创建模仿存储库
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] 
            {
                new Product { ProductID = 1, Name = "P1", Category = "Apples" } 
            }.AsQueryable());

            // 准备——创建 Cart
            Cart cart = new Cart();

            // 准备——创建控制器
            CartController target = new CartController(mock.Object, null);

            // 动作——向 Cart 添加一个产品
            RedirectToRouteResult result = target.AddToCart(cart, 2, "myUrl");

            // 断言
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");

        }

        [TestMethod]
        public void Can_View_Cart_Contents()
        {
            // 准备——创建 Cart
            Cart cart = new Cart();

            // 准备——创建控制器
            CartController target = new CartController(null, null);

            // 动作——调用 Index 动作方法
            CartIndexViewModel result = (CartIndexViewModel)target.Index(cart, "myUrl").ViewData.Model;

            // 断言
            Assert.AreEqual(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "myUrl");

        }

        [TestMethod]
        public void Cannot_Checkout_Empty_Cart()
        {
            // 准备——创建一个模仿的订单处理器
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            // 准备——创建一个空的购物车
            Cart cart = new Cart();
            // 准备——创建送货细节
            ShippingDetails shippingDetails = new ShippingDetails();
            // 布置——创建一个控制器实例
            CartController target = new CartController(null, mock.Object);

            // 动作
            ViewResult result = target.Checkout(cart, shippingDetails);

            // 断言——检查，订单尚未传递给处理器
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()),
                Times.Never());
            // 断言——检查，该方法返回的是默认视图
            Assert.AreEqual("", result.ViewName);
            // 断言——检查，对视图传递一个非法模型
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);

        }

        [TestMethod]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {
            // 准备——创建一个模仿的订单处理器
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            // 准备——创建含有一个物品的购物车
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);

            // 准备——创建一个控制器实例
            CartController target = new CartController(null, mock.Object);
            // 准备——把一个错误添加到模型
            target.ModelState.AddModelError("error", "error");

            // 动作——试图结算
            ViewResult result = target.Checkout(cart, new ShippingDetails());

            // 断言——检查，订单尚未传递给处理器
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()),
                Times.Never());
            // 断言——检查，该方法返回的是默认视图
            Assert.AreEqual("", result.ViewName);
            // 断言——检查，把一个非法模型传递给视图
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);

        }

        [TestMethod]
        public void Can_Checkout_And_Submit_Order()
        {
            // 准备——创建一个模仿的订单处理器
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            // 准备——创建含有一个物品的购物车
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);

            // 准备——创建一个控制器实例
            CartController target = new CartController(null, mock.Object);

            // 动作——试图结算
            ViewResult result = target.Checkout(cart, new ShippingDetails());

            // 断言——检查，订单已经被传递给处理器
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()),
                Times.Once());
            // 断言——检查，该方法返回的是 Completed（已完成）视图
            Assert.AreEqual("", result.ViewName);
            // 断言——检查，把一个有效模型传递给视图
            Assert.AreEqual(true, result.ViewData.ModelState.IsValid);

        }

        // 注意：对能够识别的合法送货细节不需要测试，因为这是用过模型绑定器使用
        // ShippingDetails 类属性上所运行的注解属性（特性）自动处理的。

    }
}
