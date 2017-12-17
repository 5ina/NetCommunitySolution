using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Areas.Operate.Controllers
{
    public class HomeController : AdminBaseController
    {
        // GET: Operate/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}