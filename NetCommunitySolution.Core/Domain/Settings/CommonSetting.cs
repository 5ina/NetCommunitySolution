﻿using System.ComponentModel;

namespace NetCommunitySolution.Domain.Settings
{
    public class CommonSetting
    {
        /// <summary>
        ///标题
        /// </summary>
        [DisplayName("标题")]
        public string Title { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        [DisplayName("关键字")]
        public string Keywords { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [DisplayName("说明")]
        public string Description { get; set; }

        /// <summary>
        /// 是否关闭
        /// </summary>
        [DisplayName("关闭网站")]
        public bool IsClose { get; set; }

        /// <summary>
        /// 是否开放注册
        /// </summary>
        [DisplayName("开放注册")]
        public bool IsOpenRegistration { get; set; }

        /// <summary>
        /// 注册模式
        /// </summary>
        [DisplayName("注册模式")]
        public int RegistrationId { get; set; }

        public RegistrationMode RegistrationMode
        {
            get
            {
                return (RegistrationMode)this.RegistrationId;
            }
            set
            {
                this.RegistrationId = (int)value;
            }
        }
    }
}
