using ClientFeatures.Models;
using System;
using System.Web.Mvc;

namespace ClientFeatures.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult MakeBooking()
        {
            return View(new Appointment
            {
                ClientName = "Adam",
                Date = DateTime.Now.AddDays(2),
                TermsAccepted = true
            });
        }

        [HttpPost]
        public JsonResult MakeBooking(Appointment appt)
        {
            // 在实际项目中，这里是存储新 Appointment 的语句
            return Json(appt, JsonRequestBehavior.AllowGet);
        }

    }
}
