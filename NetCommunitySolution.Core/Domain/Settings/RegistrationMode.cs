using System.ComponentModel;

namespace NetCommunitySolution.Domain.Settings
{
    /// <summary>
    /// 注册模式
    /// </summary>
    public enum RegistrationMode : int
    {
        /// <summary>
        /// Email注册
        /// </summary>
        [Description("Email注册")]
        Email = 1,

        /// <summary>
        /// 手机号注册
        /// </summary>
        [Description("手机号注册")]
        Mobile = 2,

        /// <summary>
        /// 用户名注册
        /// </summary>
        [Description("用户名注册")]
        UserName = 3,

    }
}
