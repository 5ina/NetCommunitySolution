using Abp.Runtime.Caching;
using NetCommunitySolution.Common;
using NetCommunitySolution.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Controllers
{
    public class CommonController : NetCommunitySolutionControllerBase
    {
        #region Fields && Ctor

        private readonly ICaptchaService _captchaService;
        private readonly ICacheManager _cacheManager;
        private readonly ISettingService _settingService;

        public CommonController(ICaptchaService captchaService,
            ISettingService settingService,
            ICacheManager cacheManager)
        {
            this._captchaService = captchaService;
            this._settingService = settingService;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region 验证码
        public ActionResult Captcha(int imageWidth = 200, int imageHeight = 45)
        {
            var code = "";
            var images = _captchaService.GetValidationImage(out code,imageWidth,imageHeight);
            var setting = _settingService.GetCustomerSettings();
            Session[setting.CaptchaName] = code;
            return File(images, @"image/jpeg");  //返回验证码图片
        }
        #endregion
    }
}