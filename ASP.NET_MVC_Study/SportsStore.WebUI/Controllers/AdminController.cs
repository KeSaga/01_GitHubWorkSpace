using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Controllers
{
    // 说明：
    // 过滤器试一下 .NET 的注解属性，可以把它们运用于动作方法或控制器类。
    // 当被用于控制器类时，其作用效果将覆盖当前控制器中的每一个方法。
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository _repository;

        public AdminController(IProductRepository repo)
        {
            this._repository = repo;
        }

        public ViewResult Index()
        {
            return View(this._repository.Products);
        }

        public ViewResult Edit(int productId)
        {
            Product product = _repository.Products.FirstOrDefault(p =>
                p.ProductID == productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }

                _repository.SaveProduct(product);
                // 说明：
                // TempData（临时数据）与回话数据的差别在于 TempData 在 HTTP 请求结束时会被删除。
                // ViewBag 在控制器与视图之间传递数据，但它保持数据的时间不能比当前 HTTP 请求长。（
                // 如果在用户被重定向时，则意味着用户是跨请求的，而 ViewBag 是不能用于跨请求情况下控
                // 制器与视图直接的数据传递的）我们可以使用回话数据特性，但其消息是持久的，直到它被明
                // 确的删除为止，这样的话，还不如不用。因此，我们就需要 TempData，其数据被限制到一个单
                // 一用户的会话，并且将会一直保持到被读取为止。
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // 数据值有错误
                return View(product);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Delete(int productId)
        {
            Product deletedProduct = _repository.DeleteProduct(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedProduct.Name);
            }
            return RedirectToAction("Index");
        }

    }
}
