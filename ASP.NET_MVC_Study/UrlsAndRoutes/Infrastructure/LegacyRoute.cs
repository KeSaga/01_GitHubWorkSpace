using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UrlsAndRoutes.Infrastructure
{
    public class LegacyRoute : RouteBase
    {
        private string[] urls;
        public LegacyRoute(params string[] targetUrls)
        {

        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            RouteData result = null;

            string requestedURL = httpContext.Request.AppRelativeCurrentExecutionFilePath;
            if (urls.Contains(requestedURL, StringComparer.OrdinalIgnoreCase))
            {
                result = new RouteData(this, new MvcRouteHandler());
                result.Values.Add("controller", "Legacy");
                result.Values.Add("action", "GetLegacyURL");
                result.Values.Add("legacyURL", "requestedURL");
            }
            return result;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            VirtualPathData result = null;

            if (values.ContainsKey("legactURL") && urls.Contains((string)values["legacyURL"], StringComparer.OrdinalIgnoreCase))
            {
                // 如果存在一个匹配，将会创建一个 VirtualPathData 对象，在其中传递一个对当前对象的引用和出站 URL。由于路由系统已经预先将
                // 字符“/”附加到了这个URL，因此，必须从生成的 URL 上删除这个前导字符。
                result = new VirtualPathData(this, new UrlHelper(requestContext).Content((string)values["legacyURL"]).Substring(1));
            }

            return null;
        }

    }
}