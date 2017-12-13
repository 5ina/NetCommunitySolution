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
                "Operate_default",
                "Operate/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
                name: "Operate_default",
                url: "Operate/{controller}/{action}/{id}",
                defaults: new { action = "Index", Controller = "Home", Area = "Operate", id = UrlParameter.Optional }                
            );
        }
    }
}