using System.Web.Mvc;

namespace NetCommunitySolution.Web.Controllers
{
    public class HomeController : NetCommunitySolutionControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}