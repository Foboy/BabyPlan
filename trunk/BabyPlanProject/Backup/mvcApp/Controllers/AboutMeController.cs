using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BabyPlan.mvcApp.Controllers
{
    public class AboutMeController : BaseController
    {
        //
        // GET: /AboutMe/

        public ActionResult Index()
        {
            return View("~/Views/Home/aboutme.cshtml");
        }

    }
}
