using Abp.AutoMapper;
using Abp.Runtime.Caching;
using Abp.Runtime.Session;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using NetCommunitySolution.Authentication;
using NetCommunitySolution.Authentication.Dto;
using NetCommunitySolution.Common;
using NetCommunitySolution.Customers;
using NetCommunitySolution.Domain.Settings;
using NetCommunitySolution.Web.Models.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    public class CustomerController : NetCommunitySolutionControllerBase
    {
        #region ctor && Fields
        private readonly LoginManager _loginManager;
        private readonly CustomerManager _customerManager;
        private readonly ICustomerService _customerService;
        private readonly ICacheManager _cacheManager;
        private readonly ISettingService _settingService;

        private const string CACHE_CUSTOMER_STATISTICAL_OVERVIEW = "net.cache.customer.statistical.overview";
        public CustomerController(ICustomerService customerService,
            ICacheManager cacheManager,
            LoginManager loginManager,
            CustomerManager customerManager,
            ISettingService settingService)
        {
            this._customerService = customerService;
            this._customerManager = customerManager;
            this._loginManager = loginManager;
            this._cacheManager = cacheManager;
            this._settingService = settingService;
        }
        #endregion

        #region Utilities
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        
        #endregion

        #region Login / Logout
        public ActionResult Login()
        {
            var model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {

            if (ModelState.IsValid)
            {
                var loginResult = _customerService.ValidateCustomer(model.LoginName, model.Password);
                switch (loginResult.Result)
                {
                    case LoginResults.Successful:
                        {
                            var customerDto = loginResult.Customer.MapTo<CustomerDto>();
                            //生成ClaimsIdentity
                            var identity = _loginManager.CreateUserIdentity(customerDto);

                            //用户登录
                            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = model.RememberMe }, identity);
                            

                            return RedirectToAction("Index", "Home");
                        }

                    case LoginResults.Deleted:
                        ModelState.AddModelError("", "该用户已经被冻结");
                        break;
                    case LoginResults.NotRegistered:
                        ModelState.AddModelError("", "该用户未注册");
                        break;
                    case LoginResults.Unauthorized:
                        ModelState.AddModelError("", "该用户未授权");
                        break;
                    case LoginResults.WrongPassword:
                    default:
                        ModelState.AddModelError("", "密码错误");
                        break;
                }

            }
            return View(model);

        }


        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            AbpSession = NullAbpSession.Instance;
            return Redirect("/");
        }

        #endregion

        #region Register
        
        public ActionResult Register()
        {
            var key = string.Format(NetCommunitySolutionConsts.CACHE_SETTINGS, "common");
            var commonSetting = _cacheManager.GetCache(key).Get(key, () => _settingService.GetCommonSettings());

            if (!commonSetting.IsOpenRegistration)
                return RedirectToAction("RegisterClose");

            var model = new RegisterModel();

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Register(RegisterModel model, string returnUrl, bool captchaValid, FormCollection form)
        {
            var key = string.Format(NetCommunitySolutionConsts.CACHE_SETTINGS, "common");
            var commonSetting = _cacheManager.GetCache(key).Get(key, () => _settingService.GetCommonSettings());

            if (!commonSetting.IsOpenRegistration)
                return RedirectToAction("RegisterClose");

            if (model.CheckEmail)
            {
                if (String.IsNullOrWhiteSpace(model.Email))
                    ModelState.AddModelError("", "请输入Email");
            }

            if (model.CheckLoginName)
            {
                if (String.IsNullOrWhiteSpace(model.LoginName))
                    ModelState.AddModelError("", "请输入登录名");
            }
            if (model.CheckPhone)
            {
                if (String.IsNullOrWhiteSpace(model.Phone))
                    ModelState.AddModelError("", "请输入手机号码");
            }
            if(!model.Password.Equals(model.ConfirmPassword))
                ModelState.AddModelError("", "两次密码输入的不相同");

            if (ModelState.IsValid)
            {
                _customerService.CreateCustomer(new Domain.Customers.Customer { });
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
        
        public ActionResult RegisterClose()
        {
            var model = new RegisterResultModel
            {
                Result = "对不起，社区暂时关闭注册"
            };
            return View(model);
        }



        #endregion

        #region ChildAction
        [ChildActionOnly]
        public ActionResult CustomerStatus()
        {
            var model = new SimpleCustomerModel();
            var customerId = Convert.ToInt32(AbpSession.UserId);            
            if (customerId > 0)
            {
                var customer = _customerService.GetCustomerId(customerId);
                model.Id = customer.Id;
                model.Email = customer.Email;
                model.Mobile = customer.Mobile;
                model.LoginName = customer.LoginName;
                model.NickName = customer.NickName;
            }
            return PartialView(model);
        }
        #endregion
    }
}