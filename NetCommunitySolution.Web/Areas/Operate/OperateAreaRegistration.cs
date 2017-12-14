using System.Web.Mvc;

namespace NetCommunitySolution.Web.Areas.Operate
{
    public class OperateAreaRegistration : AreaRegistration 
    {
        public override string AreaName
        {
            get
            {
                return "Operate";
            }
        }               

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Operate_default",
                url: "Operate/{controller}/{action}/{id}",
                defaults: new { action = "Index", Controller = "Home", id = UrlParameter.Optional },
                namespaces: new string[] { "NetCommunitySolution.Web.Areas.Operate.Controllers" });
        }
    }
}