using System.Web.Mvc;

namespace NetCommunitySolution.Web.Controllers
{
    public class AboutController : NetCommunitySolutionControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}