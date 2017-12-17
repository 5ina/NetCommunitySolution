using NetCommunitySolution.Domain.Settings;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Models.Customers
{
    /// <summary>
    /// 注册实体
    /// </summary>
    public partial class RegisterModel 
    {
        public bool EnabledCaptcha { get; set; }
        public string CaptchaName { get; set; }
        [DataType(DataType.Password)]
        [DisplayName("密码")]
        [AllowHtml]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("确认密码")]
        [AllowHtml]
        public string ConfirmPassword { get; set; }
        
        public bool CheckPhone { get; set; }
        [DisplayName("手机")]
        [AllowHtml]
        public string Phone { get; set; }


        public bool CheckEmail { get; set; }
        [DisplayName("Email")]
        [AllowHtml]
        public string Email { get; set; }

        public bool CheckLoginName { get; set; }
        [DisplayName("登录名")]
        [AllowHtml]
        public string LoginName { get; set; }

        public RegistrationMode RegisterMode { get; set; }


    }
}