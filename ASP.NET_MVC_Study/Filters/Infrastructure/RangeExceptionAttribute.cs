using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Filters.Infrastructure
{
    public class RangeExceptionAttribute : FilterAttribute, IExceptionFilter
    {

        public void OnException(ExceptionContext filterContext)
        {

            if (!filterContext.ExceptionHandled &&
                filterContext.Exception is ArgumentOutOfRangeException)
            {
                int val = (int)(((ArgumentOutOfRangeException)filterContext.Exception).ActualValue);
                filterContext.Result = new ViewResult
                {
                    ViewName = "RangeError",
                    ViewData = new ViewDataDictionary<int>(val)
                };
                // 下面注释掉的方式显示了一个静态页面内容
                //filterContext.Result = new RedirectResult("~/Content/RangerErrorPage.html");
                filterContext.ExceptionHandled = true;
            }
        }
    }
}