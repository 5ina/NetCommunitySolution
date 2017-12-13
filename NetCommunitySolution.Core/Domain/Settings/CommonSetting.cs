namespace NetCommunitySolution.Domain.Settings
{
    public class CommonSetting
    {
        /// <summary>
        ///标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string Keywords { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否关闭
        /// </summary>
        public bool IsClose { get; set; }

        /// <summary>
        /// 是否开放注册
        /// </summary>
        public bool IsOpenRegistration { get; set; }

        /// <summary>
        /// 注册模式
        /// </summary>
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
