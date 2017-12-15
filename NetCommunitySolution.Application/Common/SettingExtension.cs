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
        }
    }
}
