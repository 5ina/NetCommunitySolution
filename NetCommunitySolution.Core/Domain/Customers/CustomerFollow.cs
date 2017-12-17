using System;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace NetCommunitySolution.Domain.Customers
{
    /// <summary>
    /// 用户关注列表
    /// </summary>
    public class CustomerFollow : Entity, IHasCreationTime
    {
        /// <summary>
        /// 关注列表所属Id
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// 被关注者Id
        /// </summary>
        public int ConcernedCustomerId { get; set; }
        public DateTime CreationTime { get; set; }
    }
}