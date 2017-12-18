using NetCommunitySolution.CacheNames;
using NetCommunitySolution.Domain.Settings;

namespace NetCommunitySolution.Common
{
    /// <summary>
    /// 配置的扩展方法
    /// </summary>
    public static class SettingExtension
    {
        /// <summary>
        /// 获取公共配置
        /// </summary>
        /// <param name="_settingService"></param>
        /// <returns></returns>
        public static CommonSetting GetCommonSettings(this ISettingService _settingService)
        {

            var config = new CommonSetting
            {
                Description = _settingService.GetSettingByKey<string>(CommonSettingNames.Description),
                Title = _settingService.GetSettingByKey<string>(CommonSettingNames.Title),
                Keywords = _settingService.GetSettingByKey<string>(CommonSettingNames.Keywords),
                IsClose = _settingService.GetSettingByKey<bool>(CommonSettingNames.IsClose),
                IsOpenRegistration = _settingService.GetSettingByKey<bool>(CommonSettingNames.IsOpenRegistration),
                RegistrationId = _settingService.GetSettingByKey<int>(CommonSettingNames.RegistrationId),
                Name = _settingService.GetSettingByKey<string>(CommonSettingNames.Name),
                Subtitle = _settingService.GetSettingByKey<string>(CommonSettingNames.Subtitle),
            };
            return config;
        }

        /// <summary>
        /// 存储公共配置
        /// </summary>
        /// <param name="_settingService"></param>
        /// <param name="setting"></param>
        public static void SaveCommonSettings(this ISettingService _settingService, CommonSetting setting)
        {
            _settingService.SaveSetting(CommonSettingNames.Description, setting.Description);
            _settingService.SaveSetting(CommonSettingNames.Title, setting.Title);
            _settingService.SaveSetting(CommonSettingNames.Keywords, setting.Keywords);
            _settingService.SaveSetting(CommonSettingNames.IsClose, setting.IsClose);
            _settingService.SaveSetting(CommonSettingNames.IsOpenRegistration, setting.IsOpenRegistration);
            _settingService.SaveSetting(CommonSettingNames.RegistrationId, setting.RegistrationId);
            _settingService.SaveSetting(CommonSettingNames.Name, setting.Name);
            _settingService.SaveSetting(CommonSettingNames.Subtitle, setting.Subtitle);
        }

        /// <summary>
        /// 获取用户配置
        /// </summary>
        /// <param name="_settingService"></param>
        /// <returns></returns>
        public static CustomerSetting GetCustomerSettings(this ISettingService _settingService)
        {

            var config = new CustomerSetting
            {
                EnabledCaptcha = _settingService.GetSettingByKey<bool>(CustomerSettingNames.EnabledCaptcha),
                LoginNameLength = _settingService.GetSettingByKey<int>(CustomerSettingNames.LoginNameLength),
                ModifyNickName = _settingService.GetSettingByKey<bool>(CustomerSettingNames.ModifyNickName),
                PasswordMaxLength = _settingService.GetSettingByKey<int>(CustomerSettingNames.PasswordMaxLength),
                PasswordMinLength = _settingService.GetSettingByKey<int>(CustomerSettingNames.PasswordMinLength),
                CaptchaLength = _settingService.GetSettingByKey<int>(CustomerSettingNames.CaptchaLength),
                CaptchaName = _settingService.GetSettingByKey<string>(CustomerSettingNames.CaptchaName),
            };
            return config;
        }


        /// <summary>
        /// 存储用户配置
        /// </summary>
        /// <param name="_settingService"></param>
        /// <param name="setting"></param>
        public static void SaveCustomerSettings(this ISettingService _settingService, CustomerSetting setting)
        {
            _settingService.SaveSetting(CustomerSettingNames.EnabledCaptcha, setting.EnabledCaptcha);
            _settingService.SaveSetting(CustomerSettingNames.LoginNameLength, setting.LoginNameLength);
            _settingService.SaveSetting(CustomerSettingNames.ModifyNickName, setting.ModifyNickName);
            _settingService.SaveSetting(CustomerSettingNames.PasswordMaxLength, setting.PasswordMaxLength);
            _settingService.SaveSetting(CustomerSettingNames.PasswordMinLength, setting.PasswordMinLength);
            _settingService.SaveSetting(CustomerSettingNames.CaptchaName, setting.CaptchaName);
            _settingService.SaveSetting(CustomerSettingNames.CaptchaLength, setting.CaptchaLength);
        }


        /// <summary>
        /// 获取帖子配置
        /// </summary>
        /// <param name="_settingService"></param>
        /// <returns></returns>
        public static PostSetting GetPostSettings(this ISettingService _settingService)
        {

            var config = new PostSetting
            {
                PostPageSize = _settingService.GetSettingByKey<int>(PostSettingNames.PostPageSize),
                HotPostsCount = _settingService.GetSettingByKey<int>(PostSettingNames.HotPostsCount),
            };
            return config;
        }


        /// <summary>
        /// 存储贴子配置
        /// </summary>
        /// <param name="_settingService"></param>
        /// <param name="setting"></param>
        public static void SavePostSettings(this ISettingService _settingService, PostSetting setting)
        {
            _settingService.SaveSetting(PostSettingNames.HotPostsCount, setting.HotPostsCount);
            _settingService.SaveSetting(PostSettingNames.PostPageSize, setting.PostPageSize);
        }

        
        /// <summary>
        /// 获取媒体配置
        /// </summary>
        /// <param name="_settingService"></param>
        /// <returns></returns>
        public static MediaSetting GetMediaSettings(this ISettingService _settingService)
        {

            var config = new MediaSetting
            {
                AvatarFile = _settingService.GetSettingByKey<string>(MediaSettingNames.AvatarFile),
                EnabledAvatar = _settingService.GetSettingByKey<bool>(MediaSettingNames.EnabledAvatar),
                MaxAvatarSize = _settingService.GetSettingByKey<int>(MediaSettingNames.MaxAvatarSize),
                MediaMode = _settingService.GetSettingByKey<MediaMode>(MediaSettingNames.MediaMode),
                AccessKeyId = _settingService.GetSettingByKey<string>(MediaSettingNames.AccessKeyId),
                AccessKeySecret = _settingService.GetSettingByKey<string>(MediaSettingNames.AccessKeySecret),
                Bucket = _settingService.GetSettingByKey<string>(MediaSettingNames.Bucket),
                Endpoint = _settingService.GetSettingByKey<string>(MediaSettingNames.Endpoint),
            };
            return config;
        }


        /// <summary>
        /// 存储媒体配置
        /// </summary>
        /// <param name="_settingService"></param>
        /// <param name="setting"></param>
        public static void SaveMediaSettings(this ISettingService _settingService, MediaSetting setting)
        {
            _settingService.SaveSetting(MediaSettingNames.AvatarFile, setting.AvatarFile);
            _settingService.SaveSetting(MediaSettingNames.EnabledAvatar, setting.EnabledAvatar);
            _settingService.SaveSetting(MediaSettingNames.MaxAvatarSize, setting.MaxAvatarSize);
            _settingService.SaveSetting(MediaSettingNames.MediaMode, setting.MediaMode);
            _settingService.SaveSetting(MediaSettingNames.AccessKeySecret, setting.AccessKeySecret);
            _settingService.SaveSetting(MediaSettingNames.AccessKeyId, setting.AccessKeyId);
            _settingService.SaveSetting(MediaSettingNames.Bucket, setting.Bucket);
            _settingService.SaveSetting(MediaSettingNames.Endpoint, setting.Endpoint);
        }



        /// <summary>
        /// 获取媒体配置
        /// </summary>
        /// <param name="_settingService"></param>
        /// <returns></returns>
        public static RewardPointSetting GetRewardSettings(this ISettingService _settingService)
        {

            var config = new RewardPointSetting
            {
                Enabled = _settingService.GetSettingByKey<bool>(RewardPointSettingNames.Enabled),
                Comment = _settingService.GetSettingByKey<int>(RewardPointSettingNames.Comment),
               Login = _settingService.GetSettingByKey<int>(RewardPointSettingNames.Login),
                NewPost = _settingService.GetSettingByKey<int>(RewardPointSettingNames.NewPost),
                Register = _settingService.GetSettingByKey<int>(RewardPointSettingNames.Register),
                Selected = _settingService.GetSettingByKey<int>(RewardPointSettingNames.Selected),
                Solve = _settingService.GetSettingByKey<int>(RewardPointSettingNames.Solve),
            };
            return config;
        }


        /// <summary>
        /// 存储媒体配置
        /// </summary>
        /// <param name="_settingService"></param>
        /// <param name="setting"></param>
        public static void SaveRewardSettings(this ISettingService _settingService, RewardPointSetting setting)
        {
            _settingService.SaveSetting(RewardPointSettingNames.Enabled, setting.Enabled);
            _settingService.SaveSetting(RewardPointSettingNames.Comment, setting.Comment);
            _settingService.SaveSetting(RewardPointSettingNames.Login, setting.Login);
            _settingService.SaveSetting(RewardPointSettingNames.NewPost, setting.NewPost);
            _settingService.SaveSetting(RewardPointSettingNames.Register, setting.Register);
            _settingService.SaveSetting(RewardPointSettingNames.Selected, setting.Selected);
            _settingService.SaveSetting(RewardPointSettingNames.Solve, setting.Solve);
        }

    }
}
