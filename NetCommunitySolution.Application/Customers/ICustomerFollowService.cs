using Abp.Application.Services;
using Abp.Application.Services.Dto;
using NetCommunitySolution.Domain.Customers;
using System.Collections.Generic;

namespace NetCommunitySolution.Customers
{
    public interface ICustomerFollowService: IApplicationService
    {
        /// <summary>
        /// 新增关注
        /// </summary>
        /// <param name="follow"></param>
        void InsertFollow(CustomerFollow follow);

        void DeleteFollow(int followId);

        IList<CustomerFollow> GetFollowByCustomerId(int customerId);
        IList<CustomerFollow> GetFollowByConcernedId(int concernedId);

        IPagedResult<CustomerFollow> GetFollows(int concernedId = 0, int customerId = 0, int pageIndex = 0, int pageSize = int.MaxValue);


    }
}
