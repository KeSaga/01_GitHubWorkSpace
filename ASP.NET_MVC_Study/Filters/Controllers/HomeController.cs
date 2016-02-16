using Filters.Infrastructure;
using System;
using System.Collections.Generic;
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

    }
}
