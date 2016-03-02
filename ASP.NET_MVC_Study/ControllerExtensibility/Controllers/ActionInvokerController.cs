using ControllerExtensibility.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControllerExtensibility.Controllers
{
    public class ActionInvokerController : Controller
    {
        //
        // GET: /ActionInvoker/

        public ActionInvokerController()
        {
            this.ActionInvoker = new CustomActionInvoker();
        }

    }
}
