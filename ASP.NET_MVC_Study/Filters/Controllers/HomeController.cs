using Filters.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Filters.Controllers
{
    public class HomeController : Controller
    {

        /// <summary>
        /// 一个返回字符串值的动作方法，这样可以使 MVC 绕过 Razor 视图引擎，直接将字符串值发送给浏览器
        /// （这只是为了简化，在实际的项目中还是应该使用视图——这里只关注控制器）
        /// </summary>
        /// <returns></returns>
        //[CustomAuth(false)]
        [Authorize(Users = "adam,steve,jacqui", Roles = "admin")]
        public string Index()
        {
            return "This is the Index action on the Home Controller";
        }

        //自定义的异常过滤器
        //[RangeException]
        //内建的异常过滤器
        [HandleError(ExceptionType = typeof(ArgumentOutOfRangeException), View = "RangeError")]
        public string RangeTest(int id)
        {
            if (id > 100)
            {
                return string.Format("The id value is:{0}", id);
            }
            else
            {
                throw new ArgumentOutOfRangeException("id", id, "");
            }
        }

        //// ProfileAction：演示 ProfileAction 过滤器对于耗时测量的效果
        //[ProfileAction]
        //// 对 ProfileAction 动作方法过滤器作为了一个补充
        //[ProfileResult]
        //// 使用内建的动作过滤器和结果过滤器
        //[ProfileAll]
        //[CustomAction]
        public string FilterTest()
        {
            return "This is the ActionFilterTest action";
        }

        private Stopwatch timer;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            timer = Stopwatch.StartNew();
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            timer.Stop();

            filterContext.HttpContext.Response.Write(
                string.Format("<div>Total elapsed time: {0}</div>", timer.Elapsed.TotalSeconds));
        }

    }
}
