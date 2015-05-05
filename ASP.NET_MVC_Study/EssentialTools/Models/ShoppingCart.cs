using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models
{
    public class ShoppingCart
    {
        //为解决 ShoppingCart 与 LinqValueCalculator 直接的紧耦合问题，改用下面的接口形式
        //private LinqValueCalculator _calc;
        private IValueCalculator _calc;

        //为解决 ShoppingCart 与 LinqValueCalculator 直接的紧耦合问题，改用下面的接口形式
        //public ShoppingCart(LinqValueCalculator calcParam)
        //{
        //    _calc = calcParam;
        //}

        public ShoppingCart(IValueCalculator calcParam)
        {
            _calc = calcParam;
        }


        public IEnumerable<Product> Products { get; set; }

        public decimal CalculateProductTotal()
        {
            return _calc.ValueProducts(Products);
        }

    }
}