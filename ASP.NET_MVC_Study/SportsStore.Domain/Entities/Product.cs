using System;
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
        [Required(ErrorMessage = "Please enter a product name")]
        public string Name { get; set; }
        // DataType 将通知 MVC 框架，将如何显示或编辑一个值。这里的 MultilineText 表示一个多行的文本
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Please specify a category")]
        public string Category { get; set; }

        // 不需要对 ImageData 属性做任何设置，因为 MVC 框架不会对一个字节数组渲染一个编辑器。
        // （实际上这一规则只对“简单”类型起作用，如 int、string、DateTime 等）
        public byte[] ImageData { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }

    }
}
