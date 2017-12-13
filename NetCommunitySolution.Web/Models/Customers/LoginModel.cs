using Abp.Runtime.Validation;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NetCommunitySolution.Web.Models.Customers
{

    public class LoginModel : ICustomValidate
    {
        [Required]
        [DisplayName("登录名")]
        public string LoginName { get; set; }

        [Required]
        [DisplayName("密码")]
        public string Password { get; set; }

        [DisplayName("保存密码")]
        public bool RememberMe { get; set; }


        public void AddValidationErrors(CustomValidationContext context)
        {
            if (String.IsNullOrWhiteSpace(LoginName))
            {
                context.Results.Add(new ValidationResult("请输入登录账户"));
            }
        }
    }
}