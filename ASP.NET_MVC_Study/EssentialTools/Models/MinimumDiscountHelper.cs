using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models
{
    /// <summary>
    /// 这个类的功能需要实现以下几个功能，以此来演示单元测试
    /// 1、总额大于$100是，折扣为10%；
    /// 2、总额介于（并包括）$10~$100之间时，折扣为$5；
    /// 3、总额小于$10，无折扣
    /// 4、总额为负值时，抛出 ArgumentOutOfRangException
    /// </summary>
    public class MinimumDiscountHelper : IDiscountHelper
    {
        public decimal ApplyDiscount(decimal totalParam)
        {
            if (totalParam < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else if (totalParam > 100)
            {
                return totalParam * 0.9M;
            }
            else if (totalParam >= 10 && totalParam <= 100)
            {
                return totalParam - 5;
            }
            else
            {
                return totalParam;
            }
        }
    }
}