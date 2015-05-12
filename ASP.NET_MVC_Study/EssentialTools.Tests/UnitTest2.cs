using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EssentialTools.Models;
using System.Linq;

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
            var dicounter = new MinimumDiscountHelper();
            var target = new LinqValueCalculator(dicounter);
            var goalTotal = _products.Sum(e => e.Price);

            //动作
            var result = target.ValueProducts(_products);

            //断言
            Assert.AreEqual(goalTotal, result);

        }
    }
}
