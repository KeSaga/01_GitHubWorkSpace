using Filters.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Filters.Controllers
{
    public class CustomerController : Controller
    {
        [SimpleMessage(Message = "A", Order = 2)]
        [SimpleMessage(Message = "B", Order = 1)]
        public string Index()
        {
            return "This is the Customer controller";
        }

    }
}
