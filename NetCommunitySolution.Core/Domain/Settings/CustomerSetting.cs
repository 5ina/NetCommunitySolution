using System.ComponentModel;

namespace NetCommunitySolution.Domain.Settings
{
    public class CustomerSetting :ISetting
    {
        /// <summary>
        /// 是否允许修改昵称
        /// </summary>
        [DisplayName("允许修改昵称")]
        public bool ModifyNickName { get; set; }

        /// <summary>
        /// 登录名最大长度
        /// </summary>
        [DisplayName("登录名最大长度")]
        public int LoginNameLength { get; set; }

        /// <summary>
        /// 密码最大长度
        /// </summary>
        [DisplayName("密码最大长度")]
        public int PasswordMaxLength { get; set; }

        /// <summary>
        /// 密码最小长度
        /// </summary>
        [DisplayName("密码最小长度")]
        public int PasswordMinLength { get; set; }

        /// <summary>
        /// 是否启用验证码
        /// </summary>
        [DisplayName("是否启用验证码")]
        public bool EnabledCaptcha { get; set; }
    }
}
