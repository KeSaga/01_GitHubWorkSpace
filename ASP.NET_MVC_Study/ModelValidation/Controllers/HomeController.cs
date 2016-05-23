using ModelValidation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            if (string.IsNullOrEmpty(appt.ClinetName))
            {
                ModelState.AddModelError("ClinetName", "Please enter your name");
            }

            if (ModelState.IsValidField("Date") && DateTime.Now > appt.Date)
            {
                ModelState.AddModelError("Date", "Please enter a date in the future");
            }

            if (!appt.TermsAccepted)
            {
                ModelState.AddModelError("TermsAccepted", "You must accept the terms");
            }

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

    }
}
