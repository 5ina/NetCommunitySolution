using Abp.Runtime.Validation;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Models.Customers
{

    public class LoginModel : ICustomValidate
    {
        [Required]
        [DisplayName("登录名")]
        public string LoginName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("密码")]
        [AllowHtml]
        public string Password { get; set; }

        [DisplayName("保存密码")]
        [AllowHtml]
        public bool RememberMe { get; set; }

        public bool EnabledCaptcha { get; set; }

        [DisplayName("验证码")]
        public string Captcha { get; set; }

        public string CaptchaName { get; set; }


        public void AddValidationErrors(CustomValidationContext context)
        {
            if (String.IsNullOrWhiteSpace(LoginName))
            {
                context.Results.Add(new ValidationResult("请输入登录账户"));
            }
        }
    }
}