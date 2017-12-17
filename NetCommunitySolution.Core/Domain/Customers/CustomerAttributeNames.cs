namespace NetCommunitySolution.Domain.Customers
{
    /// <summary>
    /// 用户属性名称
    /// </summary>
    public static class CustomerAttributeNames
    {

        #region 基本信息
        public static string Introduce { get { return "customer.attribute.introduce"; } }
        public static string Sex { get { return "customer.attribute.sex"; } }
        public static string Avatar { get { return "customer.attribute.avatar"; } }
        /// <summary>
        /// 积分
        /// </summary>
        public static string Reward { get { return "customer.attribute.reward"; } }

        /// <summary>
        /// 积分
        /// </summary>
        public static string QQ { get { return "customer.attribute.qq"; } }
        #endregion
    }
}
