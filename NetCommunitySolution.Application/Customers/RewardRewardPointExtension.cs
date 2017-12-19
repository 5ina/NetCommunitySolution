using Abp.Application.Services.Dto;
using NetCommunitySolution.Domain.Customers;
using System;
using System.Linq;

namespace NetCommunitySolution.Customers
{
    /// <summary>
    /// 积分扩展业务类
    /// </summary>
    public static class RewardRewardPointExtension
    {
        /// <summary>
        /// 获取当前用户的积分信息
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="_rewardService"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IPagedResult<RewardPointsHistory> GetCustomerReward(this Customer customer,
            IRewardPointService _rewardService,
            int pageIndex = 0,
            int pageSize = int.MaxValue)
        {
            return _rewardService.GetAllPointsHistory(customerId: customer.Id, pageIndex: pageIndex, pageSize: pageSize);
        }

        /// <summary>
        /// 给用户增加积分
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="point"></param>
        /// <param name="mode"></param>
        /// <param name="message"></param>
        /// <param name="maxReward"></param>
        /// <returns></returns>        
        public static bool InsertCustomerRewardHistroy(this Customer customer, int point, RewardPointMode mode, string message,int maxReward )
        {
            var _rewardService = Abp.Dependency.IocManager.Instance.Resolve<IRewardPointService>();
            ///获取用户单日的总积分
            var dayRewards = _rewardService.GetAllPointsHistory(customerId: customer.Id, createdFrom: DateTime.Now);
            var allRewards = dayRewards.Items.Sum(r => r.Points);
            //判定是否大于当前积分
            if (allRewards >= maxReward)
            {
                return false;
            }

            var reward =customer.GetCustomerAttributeValue<int>(CustomerAttributeNames.Reward);

            if (point + allRewards > maxReward)
            {
                point = (allRewards + point) - maxReward;
                message += message + "(积分超出当日最高限制)";
            }

            _rewardService.InsertPoint(new RewardPointsHistory
            {
                CreationTime = DateTime.Now,
                CustomerId = customer.Id,
                Message = message,
                Points = point,
                RewardPointModeId = (short)mode,
                PointsBalance = reward + point
            });
            customer.SaveCustomerAttribute<int>(CustomerAttributeNames.Reward, reward + point);
            return true;

        }
    }
}
