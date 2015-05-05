using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models
{
    public class LinqValueCalculator : IValueCalculator
    {
        /// <summary>
        /// 计算并返回 Product 集合的所有 Product 对象的价格（Price）总和
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        public decimal ValueProducts(IEnumerable<Product> products)
        {
            return products.Sum(p => p.Price);
        }
    }
}