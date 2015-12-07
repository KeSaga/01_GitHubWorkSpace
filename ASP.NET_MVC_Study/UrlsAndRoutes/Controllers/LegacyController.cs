using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UrlsAndRoutes.Controllers
{
    /// <summary>
    /// 用以处理旧式 URL 请求的控制器
    /// </summary>
    public class LegacyController : Controller
    {

        public ActionResult GetLegacyURL(string legacyURL)
        {
            // 应用程序迁移到 MVC 之前，请求是针对文件的，因此，实际上是需要在这里处理被请求的文件。但这里
            // 只简单说明一下自定义 RouteBase 的实现原理，所以，此处仅在视图中显示这个 URL。
            return View((object)legacyURL);
        }

    }
}
