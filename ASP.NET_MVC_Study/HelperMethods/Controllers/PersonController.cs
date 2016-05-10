using HelperMethods.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelperMethods.Controllers
{
    public class PersonController : Controller
    {
        private Person[] personData =
        {
            new Person{FirstName = "Adam",LastName = "Freeman",Role = Role.Admin},
            new Person{FirstName = "Steven",LastName = "Sanderson",Role = Role.Admin},
            new Person{FirstName = "Jacqui",LastName = "Griffyth",Role = Role.User},
            new Person{FirstName = "John",LastName = "Smith",Role = Role.User},
            new Person{FirstName = "Anne",LastName = "Jones",Role = Role.Guest}
        };

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPeople()
        {
            return View(personData);
        }

        [HttpPost]
        public ActionResult GetPeople(string selectedRole)
        {
            if (selectedRole == null || selectedRole == "All")
            {
                return View(personData);
            }
            else
            {
                Role selected = (Role)Enum.Parse(typeof(Role), selectedRole);
                return View(personData.Where(p => p.Role == selected));
            }
        }

    }
}
