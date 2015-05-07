using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models
{
    public class LinqValueCalculator : IValueCalculator
    {
        private IDiscountHelper _discounter;
        public LinqValueCalculator(IDiscountHelper discountParam)
        {
            _discounter = discountParam;
        }

        /// <summary>
        /// 计算并返回 Product 集合的所有 Product 对象的价格（Price）总和
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        public decimal ValueProducts(IEnumerable<Product> products)
        {
            //改用 IDiscountHelper 接口定义的方法计算
            return _discounter.ApplyDiscount(products.Sum(p => p.Price));
            //return products.Sum(p => p.Price);
        }
    }
}