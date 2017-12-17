using NetCommunitySolution.Common;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Framework.UI
{
    public class CaptchaAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var valid = false;
            var settingService = Abp.Dependency.IocManager.Instance.Resolve<ISettingService>();
            var customerSetting = settingService.GetCustomerSettings();
            var captchaValue = filterContext.HttpContext.Request.Form[customerSetting.CaptchaName];
            var captchaInput = filterContext.HttpContext.Session[customerSetting.CaptchaName];
            if (captchaInput.ToString().ToLower().Equals(captchaValue.ToLower()))
                valid = true;
            
            filterContext.ActionParameters["captchaValid"] = valid;
            base.OnActionExecuting(filterContext);
        }
    }
}