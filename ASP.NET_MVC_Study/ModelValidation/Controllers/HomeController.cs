using ModelValidation.Models;
using System;
using System.Web.Mvc;

namespace ModelValidation.Controllers
{
    public class HomeController : Controller
    {

        public ViewResult MakeBooking()
        {
            return View(new Appointment { Date = DateTime.Now });
        }

        [HttpPost]
        public ViewResult MakeBooking(Appointment appt)
        {
            if (ModelState.IsValid)
            {
                // 在实际项目中，此处是存储库中存储新的 Appointment 的语句
                return View("Completed", appt);
            }
            else
            {
                return View();
            }

        }

        public JsonResult ValidateDate(string Date)
        {
            DateTime parsedDate;

            if (!DateTime.TryParse(Date, out parsedDate))
            {
                return Json("Please enter a valid date (yyyy/mm/dd)", JsonRequestBehavior.AllowGet);
            }
            else if (DateTime.Now > parsedDate)
            {
                return Json("Please enter a date in the future", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

        }

    }
}
