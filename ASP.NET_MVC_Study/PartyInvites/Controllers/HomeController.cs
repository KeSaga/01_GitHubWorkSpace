using PartyInvites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PartyInvites.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ViewResult Index()
        {
            int hour = DateTime.Now.Hour;
            //为视图提供数据
            ViewBag.Greeting = hour < 12 ? "Good Moring" : "Good Afternoon";
            return View();
        }

        // RsvpForm 的动作方法
        [HttpGet]
        public ViewResult RsvpForm()
        {
            return View();
        }

        [HttpPost]
        public ViewResult RsvpForm(GusetResponse guestResponse)
        {
            if (ModelState.IsValid)
            {
                // TODO: 对晚会的组织者发送 Email 响应
                return View("Thanks", guestResponse);
            }
            else
            {
                // 有验证错误
                return View();
            }
        }

    }
}
