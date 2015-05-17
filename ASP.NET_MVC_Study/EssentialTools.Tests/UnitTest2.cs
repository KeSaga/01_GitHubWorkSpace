using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EssentialTools.Models;
using System.Linq;
//使用Moq
using Moq;

namespace EssentialTools.Tests
{
    [TestClass]
    public class UnitTest2
    {
        private Product[] _products = { 
                                     new Product{Name="Kayak",Category="Watersports",Price=275M},
                                     new Product{Name="Lifejacket",Category="Watersports",Price =48.95M},
                                     new Product{Name="Soccer ball",Category="Soccer",Price=19.50M},
                                     new Product{Name="Corner flag",Category="Soccer",Price=34.95M}
                                     };

        [TestMethod]
        public void Sum_Products_Correctly()
        {
            //准备

            #region 使用 Moq

            // 1、创建模仿对象
            Mock<IDiscountHelper> mock = new Mock<IDiscountHelper>();
            // 2、选择方法:Setup，并通过 It 类设置需要的参数信息
            // 3、定义结果：Returns，并用 lambda 表达式在 Return 方法中建立具体行为
            mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>())).Returns<decimal>(total => total);
            // 4、使用模仿对象：mock.Object，通过获取 Object 属性的值来获取具体实现，如这里的实现是 IDiscountHelper 接口的实现
            var target = new LinqValueCalculator(mock.Object);

            // *** 优点：使用 Moq 会使单元测试只检查 LinqValueCalculator 对象的行为，并不依赖任何 Models 文件夹中 IDiscountHelper 的真实实现。***

            #endregion End 使用 Moq

            //var dicounter = new MinimumDiscountHelper();
            //var target = new LinqValueCalculator(dicounter);
            var goalTotal = _products.Sum(e => e.Price);

            //动作
            var result = target.ValueProducts(_products);

            //断言
            Assert.AreEqual(goalTotal, result);

        }

        private Product[] createProduct(decimal value)
        {
            return new[] { new Product { Price = value } };
        }

        /// <summary>
        /// 创建复杂的模仿对象——模拟 MinimumDiscountHelper 类的行为
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void Pass_Through_Variable_Discounts()
        {
            // 准备
            Mock<IDiscountHelper> mock = new Mock<IDiscountHelper>();
            // 全匹配
            mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>())).Returns<decimal>(total => total);
            // 模仿特定值，并抛出异常
            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v == 0))).Throws<System.ArgumentOutOfRangeException>();
            // 模仿特定值
            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v > 100))).Returns<decimal>(total => (total * 0.9M));
            // 模仿值的范围
            mock.Setup(m => m.ApplyDiscount(It.IsInRange<decimal>(10, 100, Range.Inclusive))).Returns<decimal>(total => total - 5);
            // 上面的写法等同于下面这种方式（这种方式通常更灵活）：
            //mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v >= 10 && v <= 100))).Returns<decimal>(total => total - 5);

            // ********** 注意 ******* 注意 ********* 注意 ********* 注意 **********
            // 上述的模仿顺序是不能颠倒的，原因如下：
            // Moq 会以相反的顺序评估所给定的行为，因此会考虑调用最后一个 Setup 方法。也就是说我们在使用时必须遵循先从最一般开始向最特殊的情绪顺序就能行。
            // 即：Moq 会先去评定最后的，然后上逐步执行。按照此例，如果最普通的情况先执行，就意味着所有符合 decimal 类型参数的情形均可满足条件，那后续的如
            // 大于 100 时将不会返回预期结果，而是错误的模仿结果了。
            // ********** 注意 ******* 注意 ********* 注意 ********* 注意 **********

            var target = new LinqValueCalculator(mock.Object);

            // 动作
            decimal FiveDollarDiscount = target.ValueProducts(createProduct(5));
            decimal TenDollarDiscount = target.ValueProducts(createProduct(10));
            decimal FiftyDollarDiscount = target.ValueProducts(createProduct(50));
            decimal HundredDollarDiscount = target.ValueProducts(createProduct(100));
            decimal FiveHundredDollarDiscount = target.ValueProducts(createProduct(5000));

            // 断言
            Assert.AreEqual(5, FiveDollarDiscount, "$5 Fail");
            Assert.AreEqual(5, TenDollarDiscount, "$10 Fail");
            Assert.AreEqual(45, FiftyDollarDiscount, "$50 Fail");
            Assert.AreEqual(95, HundredDollarDiscount, "$100 Fail");
            Assert.AreEqual(450, FiveHundredDollarDiscount, "$500 Fail");
            target.ValueProducts(createProduct(0));

        }

    }
}
