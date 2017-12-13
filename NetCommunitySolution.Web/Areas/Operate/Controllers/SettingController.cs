using Abp.Runtime.Caching;
using NetCommunitySolution.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            return View();
        }
        #endregion

    }
}