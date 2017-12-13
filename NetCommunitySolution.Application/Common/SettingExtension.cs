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
        /// 获取微信的配置信息
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

        public static void SaveCommonSettings(this ISettingService _settingService, CommonSetting setting)
        {
            _settingService.SaveSetting(CommonSettingNames.Description, setting.Description);
            _settingService.SaveSetting(CommonSettingNames.Title, setting.Title);
            _settingService.SaveSetting(CommonSettingNames.Keywords, setting.Keywords);
            _settingService.SaveSetting(CommonSettingNames.IsClose, setting.IsClose);
            _settingService.SaveSetting(CommonSettingNames.IsOpenRegistration, setting.IsOpenRegistration);
            _settingService.SaveSetting(CommonSettingNames.RegistrationId, setting.RegistrationId);
        }
    }
}
