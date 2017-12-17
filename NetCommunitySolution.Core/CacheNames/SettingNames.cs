namespace NetCommunitySolution.CacheNames
{
    /// <summary>
    /// 公共配置
    /// </summary>
    public class CommonSettingNames
    {
        /// <summary>
        /// 网站名称
        /// </summary>
        public static string Name { get { return "net.setting.common.name"; } }
        /// <summary>
        /// 副标题
        /// </summary>
        public static string Subtitle { get { return "net.setting.common.subtitle"; } }
        /// <summary>
        /// 项目Title
        /// </summary>
        public static string Title { get { return "net.setting.common.title"; } }

        /// <summary>
        /// 项目Keywords
        /// </summary>
        public static string Keywords { get { return "net.setting.common.keywords"; } }

        /// <summary>
        /// 项目Description
        /// </summary>
        public static string Description { get { return "net.setting.common.description"; } }
        /// <summary>
        /// 是否关闭
        /// </summary>
        public static string IsClose { get { return "net.setting.common.isclose"; } }
        /// <summary>
        /// 是否关闭
        /// </summary>
        public static string IsOpenRegistration { get { return "net.setting.common.isopenregistation"; } }
        /// <summary>
        /// 注册模式
        /// </summary>
        public static string RegistrationId { get { return "net.setting.common.registrationid"; } }
        

    }

    /// <summary>
    /// 用户配置
    /// </summary>
    public class CustomerSettingNames
    {
        /// <summary>
        /// 是否允许修改昵称
        /// </summary>
        public static string ModifyNickName { get { return "net.setting.customer.modify.nickname"; } }

        /// <summary>
        /// 登录名最大长度
        /// </summary>
        public static string LoginNameLength { get { return "net.setting.customer.loginnamelength"; } }

        /// <summary>
        /// 密码最大长度
        /// </summary>
        public static string PasswordMaxLength { get { return "net.setting.customer.passwordmaxlength"; } }

        /// <summary>
        /// 密码最小长度
        /// </summary>
        public static string PasswordMinLength { get { return "net.setting.customer.passwordminlength"; } }

        /// <summary>
        /// 是否启用验证码
        /// </summary>
        public static string EnabledCaptcha { get { return "net.setting.customer.enabledcaptcha"; } }
        /// <summary>
        /// 验证码长度
        /// </summary>
        public static string CaptchaLength { get { return "net.setting.customer.captchalength"; } }

        /// <summary>
        /// 验证码名称
        /// </summary>
        public static string CaptchaName { get { return "net.setting.customer.captchaname"; } }


    }

    /// <summary>
    /// 帖子配置
    /// </summary>
    public class PostSettingNames
    {
        /// <summary>
        /// 热门帖子个数
        /// </summary>
        public static string HotPostsCount { get { return "net.setting.post.hotpostscount"; } }

        /// <summary>
        /// 帖子分页个数
        /// </summary>
        public static string PostPageSize { get { return "net.setting.post.pagesize"; } }
    }


    /// <summary>
    /// 帖子配置
    /// </summary>
    public class MediaSettingNames
    {
        /// <summary>
        /// 是否启用头像
        /// </summary>
        public static string EnabledAvatar { get { return "net.setting.media.enabledavatar"; } }

        /// <summary>
        /// 媒体存储模式
        /// </summary>
        public static string MediaMode { get { return "net.setting.media.mediamode"; } }


        /// <summary>
        /// 头像最大限制
        /// </summary>
        public static string MaxAvatarSize { get { return "net.setting.media.maxavatarsize"; } }

        /// <summary>
        /// 头像图像文件类型
        /// </summary>
        public static string AvatarFile { get { return "net.setting.media.avatarfile"; } }


        /// <summary>
        /// 头像图像文件类型
        /// </summary>
        public static string AccessKeyId { get { return "net.setting.media.accesskeyid"; } }
        /// <summary>
        /// 头像图像文件类型
        /// </summary>
        public static string AccessKeySecret { get { return "net.setting.media.accesskeysecret"; } }
        /// <summary>
        /// 头像图像文件类型
        /// </summary>
        public static string Endpoint { get { return "net.setting.media.endpoint"; } }
        /// <summary>
        /// bucket
        /// </summary>
        public static string Bucket { get { return "net.setting.media.bucket"; } }
    }
}
