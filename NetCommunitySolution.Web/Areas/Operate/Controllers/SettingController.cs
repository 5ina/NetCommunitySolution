using Abp.Runtime.Caching;
using NetCommunitySolution.Common;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Areas.Operate.Controllers
{
    public class SettingController : AdminBaseController
    {
        #region Fields && Ctor

        private readonly ISettingService _settingService;
        private readonly ICacheManager _cacheManager;

        public SettingController(ISettingService settingService, ICacheManager cacheManager)
        {
            this._settingService = settingService;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Utilities

        #endregion

        #region Method
        public ActionResult Common()
        {
            var model = _settingService.GetCommonSettings();
            return View(model);
        }

        [HttpPost]
        public ActionResult Common(Domain.Settings.CommonSetting model)
        {
            _settingService.SaveCommonSettings(model);
            ViewBag.Result = "true";
            return View();
        }


        public ActionResult CustomerSetting()
        {
            var model = _settingService.GetCustomerSettings();
            return View(model);
        }

        [HttpPost]
        public ActionResult CustomerSetting(Domain.Settings.CustomerSetting model)
        {
            _settingService.SaveCustomerSettings(model);
            ViewBag.Result = "true";
            return View();
        }


        public ActionResult PostSetting()
        {
            var model = _settingService.GetPostSettings();
            return View(model);
        }

        [HttpPost]
        public ActionResult PostSetting(Domain.Settings.PostSetting model)
        {
            _settingService.SavePostSettings(model);
            ViewBag.Result = "true";
            return View();
        }
        #endregion

    }
}