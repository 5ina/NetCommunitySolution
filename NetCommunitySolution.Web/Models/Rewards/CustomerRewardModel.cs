using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using NetCommunitySolution.Domain.Customers;
using NetCommunitySolution.Web.Models.Common;
using System;

namespace NetCommunitySolution.Web.Models.Rewards
{
    public class CustomerRewardModel:EntityDto
    {
        public CustomerRewardModel()
        {
            this.Items = new PagedItemModel<RewardPointsHistoryModel>();
        }
        public int Reward { get; set; }
        
        public PagedItemModel<RewardPointsHistoryModel> Items { get; set; }
    }
    

    [AutoMap(typeof(RewardPointsHistory))]
    public class RewardPointsHistoryModel : EntityDto
    {

        /// <summary>
        /// 所属用户
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// 积分模式
        /// </summary>
        public Int16 RewardPointModeId { get; set; }

        public RewardPointMode RewardPointMode { get { return (RewardPointMode)this.RewardPointModeId; }
 }

        /// <summary>
        /// 变动积分的数量
        /// </summary>
        public int Points { get; set; }

        /// <summary>
        /// 变动后的积分
        /// </summary>
        public int PointsBalance { get; set; }
        public string Message { get; set; }
        public DateTime CreationTime { get; set; }
    }
}