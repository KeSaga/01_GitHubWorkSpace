using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Filters.Infrastructure
{
    public class CustomActionAttribute : FilterAttribute, IActionFilter
    {
        private Stopwatch timer;

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsLocal)
            {
                filterContext.Result = new HttpNotFoundResult();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContex"></param>
        /// <remarks>
        /// 如果不需要实现任何逻辑，则空着即可。注意不要抛出 NotImplementedExcetion 异常 —— 否则 MVC 将触发异常过滤器
        /// </remarks>
        public void OnActionExecuted(ActionExecutedContext filterContex)
        {
            // 尚未实现

        }

    }
}