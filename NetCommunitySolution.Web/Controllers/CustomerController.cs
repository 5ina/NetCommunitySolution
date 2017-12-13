using Abp.AutoMapper;
using Abp.Runtime.Caching;
using Abp.Runtime.Session;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using NetCommunitySolution.Authentication;
using NetCommunitySolution.Authentication.Dto;
using NetCommunitySolution.Customers;
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

        private const string CACHE_CUSTOMER_STATISTICAL_OVERVIEW = "net.cache.customer.statistical.overview";
        public CustomerController(ICustomerService customerService,
            ICacheManager cacheManager,
            LoginManager loginManager,
            CustomerManager customerManager)
        {
            this._customerService = customerService;
            this._customerManager = customerManager;
            this._loginManager = loginManager;
            this._cacheManager = cacheManager;
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
    }
}