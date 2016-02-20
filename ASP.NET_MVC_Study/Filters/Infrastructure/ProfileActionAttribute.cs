using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Filters.Infrastructure
{
    public class ProfileActionAttribute : FilterAttribute, IActionFilter
    {
        private Stopwatch timer;

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            timer = Stopwatch.StartNew();
        }

        public void OnActionExecuted(ActionExecutedContext filterContex)
        {
            timer.Stop();

            if (filterContex.Exception == null)
            {
                filterContex.HttpContext.Response.Write(
                    string.Format("<div>Action method elapsed time: {0}</div>", timer.Elapsed.TotalSeconds));
            }

        }

    }
}