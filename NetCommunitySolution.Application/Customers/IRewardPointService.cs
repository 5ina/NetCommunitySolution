using Abp.Application.Services;
using Abp.Application.Services.Dto;
using NetCommunitySolution.Domain.Customers;
using System;
using System.Collections.Generic;

namespace NetCommunitySolution.Customers
{
    /// <summary>
    /// 积分服务接口
    /// </summary>
    public interface IRewardPointService: IApplicationService
    {
        /// <summary>
        /// 新增积分
        /// </summary>
        /// <param name="point"></param>
        void InsertPoint(RewardPointsHistory point);

        /// <summary>
        /// 更新积分
        /// </summary>
        /// <param name="point"></param>
        void UpdatePoint(RewardPointsHistory point);

        /// <summary>
        /// 删除积分记录
        /// </summary>
        /// <param name="pointId"></param>
        void DeletePoint(int pointId);


        /// <summary>
        /// 获取积分记录根据Id
        /// </summary>
        /// <param name="pointId"></param>
        /// <returns></returns>
        RewardPointsHistory GetPointsHistoryById(int pointId);

        /// <summary>
        /// 获取所有积分记录
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="createdFrom"></param>
        /// <param name="createdTo"></param>
        /// <param name="customerId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IPagedResult<RewardPointsHistory> GetAllPointsHistory(string keywords = "",
            DateTime? createdFrom = null,
            DateTime? createdTo = null,
            int customerId = 0,
            int pageIndex = 0,
            int pageSize = int.MaxValue);

        IList<RewardPointsHistory> GetPointsHistoryByCustomerId(int customerId = 0);
    }
}
