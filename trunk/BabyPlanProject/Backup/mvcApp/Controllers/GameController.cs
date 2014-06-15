using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BabyPlan.mvcApp.Controllers
{
    public class GameController : BaseController
    {
        //
        // GET: /Game/

        public ActionResult Index()
        {
            return View("~/Views/Game/Index.cshtml");
        }

        public ActionResult Vote()
        {
            return View("~/Views/Game/Vote.cshtml");
        }

        public ActionResult Challenge()
        {
            return View("~/Views/Game/Challenge.cshtml");
        }
    }
}
