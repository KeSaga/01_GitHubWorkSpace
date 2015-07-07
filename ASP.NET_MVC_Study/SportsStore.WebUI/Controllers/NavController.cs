using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;

namespace SportsStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        //
        // GET: /Nav/

        private IProductRepository _repository;

        public NavController(IProductRepository repo)
        {
            _repository = repo;
        }

        public PartialViewResult Menu(string category = null)
        {
            // 实现高亮显示当前分类。可以使用视图模型或视图包的形式实现，下面的方式选择了视图包的方式实现。
            // 该方法增加的可选参数 category 将由路由配置自动提供值；ViewBag 对象动态创建了一个
            // SelectedCategory 属性（ViewBag 是一个动态对象，它可以简单地创建新的属性）。
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = _repository.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);

            return PartialView(categories);
        }

    }
}
