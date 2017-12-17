using Abp.AutoMapper;
using Abp.Runtime.Caching;
using Abp.Runtime.Session;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using NetCommunitySolution.Authentication;
using NetCommunitySolution.Authentication.Dto;
using NetCommunitySolution.CacheNames;
using NetCommunitySolution.Common;
using NetCommunitySolution.Customers;
using NetCommunitySolution.Domain.Customers;
using NetCommunitySolution.Domain.Settings;
using NetCommunitySolution.Media;
using NetCommunitySolution.Security;
using NetCommunitySolution.Web.Framework.Controllers;
using NetCommunitySolution.Web.Models.Customers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NetCommunitySolution.Web.Framework.UI;

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
        private readonly IOssService _ossService;
        private readonly ICustomerAttributeService _attribteService;
        private readonly IEncryptionService _encryptionService;

        private const string CACHE_CUSTOMER_STATISTICAL_OVERVIEW = "net.cache.customer.statistical.overview";
        public CustomerController(ICustomerService customerService,
            ICacheManager cacheManager,
            LoginManager loginManager,
            CustomerManager customerManager,
            ISettingService settingService,
            IOssService ossService,
            ICustomerAttributeService attribteService,
            IEncryptionService encryptionService)
        {
            this._customerService = customerService;
            this._customerManager = customerManager;
            this._loginManager = loginManager;
            this._cacheManager = cacheManager;
            this._settingService = settingService;
            this._attribteService = attribteService;
            this._encryptionService = encryptionService;
            this._ossService = ossService;
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

        [NonAction]
        protected Customer GetCurrentCustomer()
        {
            var customerId = Convert.ToInt32(AbpSession.UserId);
            if (customerId > 0)
                return _customerService.GetCustomerId(customerId);
            return null;
        }

        [NonAction]
        protected virtual void PrepareLoginModel(LoginModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            var customerSetting = _settingService.GetCustomerSettings();
            model.EnabledCaptcha = customerSetting.EnabledCaptcha;
            model.CaptchaName = customerSetting.CaptchaName;
        }

        [NonAction]
        protected virtual void PrepareCustomerInfoModel(CustomerInfoModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            var customerSetting = _settingService.GetCustomerSettings();
            model.EnabledModifyName = customerSetting.ModifyNickName;
        }

        [NonAction]
        protected virtual void PrepareCustomerAttributeModel(CustomerInfoModel model,Customer customer)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            var attributeModel = customer.GetCustomerAttributes(_attribteService);
            model.QQ = attributeModel.QQ;
            model.Introduce = attributeModel.Introduce;
        }
        #endregion

        #region Login / Logout

        public ActionResult Login()
        {
            var model = new LoginModel();
            PrepareLoginModel(model);
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [Captcha]
        public ActionResult Login(LoginModel model,bool captchaValid)
        {
            if (model.EnabledCaptcha && !captchaValid)
            {
                ModelState.AddModelError("", "验证码错误");
            }

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
            PrepareLoginModel(model);
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
            var customerSetting = _cacheManager.GetCache(string.Format(NetCommunitySolutionConsts.CACHE_SETTINGS, "customer"))
                .Get(string.Format(NetCommunitySolutionConsts.CACHE_SETTINGS, "customer"),
                () => _settingService.GetCustomerSettings());

            model.EnabledCaptcha = customerSetting.EnabledCaptcha;
            model.CaptchaName = customerSetting.CaptchaName;

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [Captcha]
        public ActionResult Register(RegisterModel model, bool captchaValid)
        {
            if (model.EnabledCaptcha && !captchaValid)
            {
                ModelState.AddModelError("", "验证码错误");
            }
            var key = string.Format(NetCommunitySolutionConsts.CACHE_SETTINGS, "common");
            var commonSetting = _cacheManager.GetCache(key).Get(key, () => _settingService.GetCommonSettings());

            if (!commonSetting.IsOpenRegistration)
                return RedirectToAction("RegisterClose");

            if (model.RegisterMode == RegistrationMode.Email)
            {
                if (String.IsNullOrWhiteSpace(model.Email))
                    ModelState.AddModelError("", "请输入Email");
                model.LoginName = model.Email;
            }

            if (model.RegisterMode == RegistrationMode.UserName)
            {
                if (String.IsNullOrWhiteSpace(model.LoginName))
                    ModelState.AddModelError("", "请输入登录名");                
            }
            if (model.RegisterMode == RegistrationMode.Mobile)
            {
                if (String.IsNullOrWhiteSpace(model.Phone))
                    ModelState.AddModelError("", "请输入手机号码");
                model.LoginName = model.Phone;
            }
            if(!model.Password.Equals(model.ConfirmPassword))
                ModelState.AddModelError("", "两次密码输入的不相同");

            if (ModelState.IsValid)
            {
                var salt = CommonHelper.GenerateCode(6);
                var password = _encryptionService.CreatePasswordHash(model.Password, salt);
                var customer = new Domain.Customers.Customer
                {
                    LoginName = model.LoginName,
                    Mobile = model.Phone,
                    Email = model.Email,
                    CustomerRoleId = (int)CustomerRole.Member,
                    Level = 0,
                    NickName = "",
                    Password = password,
                    PasswordSalt = salt,
                    CreationTime = DateTime.Now,
                    LastModificationTime = DateTime.Now,
                    VerificatExpiryTime = DateTime.Now,
                };
                customer.Id = _customerService.CreateCustomer(customer);

                var dto = customer.MapTo<CustomerDto>();
                var identity = _loginManager.CreateUserIdentity(dto);
                AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = false }, identity);
                return RedirectToAction("Index", "Home");
            }

            var customerSetting = _cacheManager.GetCache(string.Format(NetCommunitySolutionConsts.CACHE_SETTINGS, "customer"))
                .Get(string.Format(NetCommunitySolutionConsts.CACHE_SETTINGS, "customer"),
                () => _settingService.GetCustomerSettings());

            model.EnabledCaptcha = customerSetting.EnabledCaptcha;
            model.CaptchaName = customerSetting.CaptchaName;
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
        public ActionResult CustomerBoxInfo(int customerId)
        {
            var customer = _customerService.GetCustomerId(customerId);
            var model = customer.GetCustomerAttributes(_attribteService);
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult CustomerStatus()
        {
            var model = new SimpleCustomerModel();
            var commonSettings = _settingService.GetCommonSettings();
            model.Name = commonSettings.Name;
            model.SubTitle = commonSettings.Subtitle;
            var customerId = Convert.ToInt32(AbpSession.UserId);            
            if (customerId > 0)
            {
                var customer = _customerService.GetCustomerId(customerId);
                model.Id = customer.Id;
                model.Email = customer.Email;
                model.Mobile = customer.Mobile;
                model.LoginName = customer.LoginName;
                model.NickName = customer.NickName;
                model.CustomerRole = (CustomerRole)customer.CustomerRoleId;
            }
            return PartialView(model);
        }
        #endregion

        #region Change Password / Info
        [MemberLogin(true)]
        public ActionResult ChangePassword()
        {
            var customer = GetCurrentCustomer();
            var model = new ChangePasswordModel();
            model.Id = customer.Id;
            model.LoginName = model.LoginName;
            return View(model);
        }

        [MemberLogin(true)]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {

            if (!model.ConfirmNewPassword.Equals(model.NewPassword))
                ModelState.AddModelError("", "两次新密码输入的不相同");

            var customer = GetCurrentCustomer();
            var password = _encryptionService.CreatePasswordHash(model.OldPassword, customer.PasswordSalt);
            if(!customer.Password.Equals(password))
                ModelState.AddModelError("", "原密码输入错误");

            if (ModelState.IsValid)
            {
                var newPassword = _encryptionService.CreatePasswordHash(model.NewPassword, customer.PasswordSalt);
                customer.Password = newPassword;
                _customerService.UpdateCustomer(customer);
                model.Result = "密码修改成功";


            }
            return View(model);
        }

        [MemberLogin(true)]
        public ActionResult Info()
        {
            var customer = GetCurrentCustomer();
            var model = customer.MapTo<CustomerInfoModel>();
            PrepareCustomerInfoModel(model);
            PrepareCustomerAttributeModel(model, customer);
            return View(model);
        }

        [MemberLogin(true)]
        [HttpPost]
        public ActionResult Info(CustomerInfoModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = _customerService.GetCustomerId(model.Id);
                customer = model.MapTo<CustomerInfoModel, Customer>(customer);
                _customerService.UpdateCustomer(customer);

                customer.SaveCustomerAttribute<string>(CustomerAttributeNames.QQ, model.QQ);

                model.Result = "true";
            }

            return View(model);
        }

        [MemberLogin(true)]
        public ActionResult Avatar()
        {
            var customer = GetCurrentCustomer();
            var mediaSetting = _settingService.GetMediaSettings();
            var avatar = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.Avatar);
            var model = new CustomerAvatarModel
            {
                Id = customer.Id,
                AvatarUrl = avatar,
                MaxSize = mediaSetting.MaxAvatarSize,
                Exts = mediaSetting.AvatarFile
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult UploadAvatar()
        {
            if (Request.Files.Count <= 0)
                return AbpJson(null);
            
            HttpPostedFileBase httpPostedFile = Request.Files[0];
            Stream stream = httpPostedFile.InputStream;
            var fileName = Path.GetFileName(httpPostedFile.FileName);
            var contentType = httpPostedFile.ContentType;

            var fileBinary = new byte[stream.Length];
            stream.Read(fileBinary, 0, fileBinary.Length);

            var fileExtension = Path.GetExtension(fileName);
            if (!String.IsNullOrEmpty(fileExtension))
                fileExtension = fileExtension.ToLowerInvariant();
            var mediaMode = _settingService.GetSettingByKey<MediaMode>(MediaSettingNames.MediaMode);
            var url = string.Empty;
            if (mediaMode == MediaMode.Local)
                url = "/images/default_avatar.png";
            else if (mediaMode == MediaMode.Alyun)
                url = _ossService.UploadImage(images: fileBinary, isBuildThumbnail: false);


            var customer = GetCurrentCustomer();
            customer.SaveCustomerAttribute<string>(CustomerAttributeNames.Avatar, url);

            return AbpJson(new
            {
                code = 0,
                msg = "",
                data = new
                {
                    src = url
                }
            });
        }

        [ChildActionOnly]
        public ActionResult CustomerNavigation(int selectedTabId = 0)
        {
            var model = new CustomerNavigationModel();

            model.CustomerNavigationItems.Add(new CustomerNavigationItemModel
            {
                Url = Url.Action("Info","Customer" ),
                Title = "会员中心",
                Tab = CustomerNavigationEnum.Info,
                ItemClass = "customer-info"
            });
            
                model.CustomerNavigationItems.Add(new CustomerNavigationItemModel
                {
                    Url = Url.Action("MyPost", "Post"),
                    Title ="我的主题",
                    Tab = CustomerNavigationEnum.Posts,
                    ItemClass = "customer-post"
                });
            
                model.CustomerNavigationItems.Add(new CustomerNavigationItemModel
                {
                    Url = Url.Action("Reward", "Customer"),
                    Title = "我的积分",
                    Tab = CustomerNavigationEnum.RewardPoints,
                    ItemClass = "customer-reward"
                });

            var mediaSetting = _settingService.GetMediaSettings();
            if(mediaSetting.EnabledAvatar)
            {
                model.CustomerNavigationItems.Add(new CustomerNavigationItemModel
                {
                    Url = Url.Action("Avatar", "Customer"),
                    Title = "我的头像",
                    Tab = CustomerNavigationEnum.Avatar,
                    ItemClass = "customer-avatar"
                });

            }

            model.CustomerNavigationItems.Add(new CustomerNavigationItemModel
            {
                Url = Url.Action("ChangePassword", "Customer"),
                Title = "修改密码",
                Tab = CustomerNavigationEnum.ChangePassword,
                ItemClass = "change-password"
            });
            

            model.SelectedTab = (CustomerNavigationEnum)selectedTabId;

            return PartialView(model);
        }
        #endregion

        #region No Login

        public ActionResult NotLogged()
        {
            return View();
        }
        #endregion

    }

}