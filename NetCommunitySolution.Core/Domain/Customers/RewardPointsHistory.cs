using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;

namespace NetCommunitySolution.Domain.Customers
{
    /// <summary>
    /// 积分记录
    /// </summary>
    public class RewardPointsHistory : Entity, IHasCreationTime
    {
        /// <summary>
        /// 所属用户
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// 积分模式
        /// </summary>
        public Int16 RewardPointModeId { get; set; }
        
        /// <summary>
        /// 变动积分的数量
        /// </summary>
        public int Points { get; set; }

        /// <summary>
        /// 变动后的积分
        /// </summary>
        public int PointsBalance { get; set; }
        [Required, MaxLength(200)]
        public string Message { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
