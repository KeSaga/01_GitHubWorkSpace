﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SportsStore.Domain.Entities
{
    public class Product
    {
        // HiddenInput 将通知 MVC 框架，将该属性渲染为隐藏的表单元素
        [HiddenInput(DisplayValue = false)]
        public int ProductID { get; set; }
        public string Name { get; set; }
        // DataType 将通知 MVC 框架，将如何显示或编辑一个值。这里的 MultilineText 表示一个多行的文本
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }

    }
}
