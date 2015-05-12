using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EssentialTools.Models;

namespace EssentialTools.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private IDiscountHelper getTestObject()
        {
            return new MinimumDiscountHelper();
        }

        /// <summary>
        /// 像下面使用 [TestMethod] 特性标签注释的方法才是单元测试的方法，而测试类中没有这种注释的方法
        /// 不会被 Visual Studio 当作单元测试的（如上面的getTestObject()方法）。
        /// </summary>
        [TestMethod]
        public void Discount_Above_100()
        {
            //准备
            IDiscountHelper target = getTestObject();
            decimal total = 200;

            //动作
            var discountedTotal = target.ApplyDiscount(total);

            //断言
            Assert.AreEqual(total * 0.9M, discountedTotal);
        }

        [TestMethod]
        public void Discount_Between_10_And_100()
        {
            //准备
            IDiscountHelper target = getTestObject();

            //动作
            decimal TenDollarDiscount = target.ApplyDiscount(10);
            decimal HundredDollarDiscount = target.ApplyDiscount(100);
            decimal FiftyDollarDiscount = target.ApplyDiscount(50);

            //断言
            Assert.AreEqual(5, TenDollarDiscount, "$10 discount is wrong");
            Assert.AreEqual(95, HundredDollarDiscount, "$100 discount is wrong");
            Assert.AreEqual(45, FiftyDollarDiscount, "$50 discount is wrong");
        }

        [TestMethod]
        public void Discount_Less_Than_10()
        {
            //准备
            IDiscountHelper target = getTestObject();

            //动作
            decimal discount5 = target.ApplyDiscount(5);
            decimal discount0 = target.ApplyDiscount(0);

            //断言
            Assert.AreEqual(5, discount5);
            Assert.AreEqual(0, discount0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Discount_Negative_Total()
        {
            //准备
            IDiscountHelper target = getTestObject();

            //动作
            target.ApplyDiscount(-1);
        }

    }
}
