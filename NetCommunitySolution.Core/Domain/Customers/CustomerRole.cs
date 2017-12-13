using System.ComponentModel;

namespace NetCommunitySolution.Domain.Customers
{
    /// <summary>
    /// 用户角色
    /// </summary>
    public enum CustomerRole : int
    {
        /// <summary>
        /// 供应商
        /// </summary>
        [Description("用户")]
        Member = 1,
        /// <summary>
        /// 系统管理员
        /// </summary>
        [Description("管理员")]
        System = 2,
    }
}