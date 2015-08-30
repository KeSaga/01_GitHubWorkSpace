using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _repository;
        public int PageSize = 4;

        /// <summary>
        /// 该构造函数将允许 Ninject 在实例化这个控制器类时注入产品存储库的依赖性
        /// </summary>
        /// <param name="productRepository"></param>
        public ProductController(IProductRepository productRepository)
        {
            this._repository = productRepository;
        }

        public ViewResult List(string category, int page = 1)
        {
            //return View(_repository.Products.OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize));

            // 使用视图模型数据
            ProductsListViewModel model = new ProductsListViewModel
            {
                // Where 条件：如果 category 非空，则只选出与 Category 属性匹配的那些 Product 对象
                Products = _repository.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                    _repository.Products.Count() :
                    _repository.Products.Where(e => e.Category == category).Count()
                },
                CurrentCategory = category
            };

            return View(model);
        }

        public FileContentResult GetImage(int productId)
        {
            Product prod = _repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (prod != null)
            {
                return File(prod.ImageData, prod.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

    }
}
