using Abp.Web.Mvc.Controllers;

namespace NetCommunitySolution.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class NetCommunitySolutionControllerBase : AbpController
    {
        protected NetCommunitySolutionControllerBase()
        {
            LocalizationSourceName = NetCommunitySolutionConsts.LocalizationSourceName;
        }
    }
}