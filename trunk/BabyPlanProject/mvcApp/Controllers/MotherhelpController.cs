using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BabyPlan.mvcApp.Controllers
{
    public class MotherhelpController : Controller
    {
        //
        // GET: /Motherhelp/

        public ActionResult Index()
        {
            return View("~/Views/info/motherhelp.cshtml");
        }

    }
}
