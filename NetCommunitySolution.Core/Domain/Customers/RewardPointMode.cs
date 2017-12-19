using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCommunitySolution.Domain.Customers
{
    /// <summary>
    /// 积分变动模式
    /// </summary>
    public enum RewardPointMode : int
    {
        [Description("获得积分")]
        Get = 1,

        [Description("消费积分")]
        Lose = 2,
    }
}
