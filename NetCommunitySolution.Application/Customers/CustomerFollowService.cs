using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using NetCommunitySolution.Domain.Customers;

namespace NetCommunitySolution.Customers
{
    public class CustomerFollowService : NetCommunitySolutionAppServiceBase, ICustomerFollowService
    {
        #region Ctor && Field

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : Customer ID
        /// {1} : ConcernedCustomer ID
        /// </remarks>
        private const string FOLLOWS_BY_CUSTOMERID = "net.follow.by-customer-id.{0}.{1}";
        private const string FOLLOWS_PATTERN_KEY = "net.follow.";


        private readonly IRepository<CustomerFollow> _followRepository;
        private readonly ICacheManager _cacheManager;
        public CustomerFollowService(
            IRepository<CustomerFollow> followRepository,
            ICacheManager cacheManager)
        {
            this._followRepository = followRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region method
        public void DeleteFollow(int followId)
        {
            if (followId > 0)
                _followRepository.Delete(followId);
            _cacheManager.RemoveByPattern(FOLLOWS_PATTERN_KEY);
        }

        public IList<CustomerFollow> GetFollowByConcernedId(int concernedId)
        {
            var key = string.Format(FOLLOWS_BY_CUSTOMERID, 0, concernedId);
            return _cacheManager.GetCache(key).Get(key, () =>
            {
                return _followRepository.GetAllList(f => f.ConcernedCustomerId == concernedId);
            }).ToList();
        }

        public IList<CustomerFollow> GetFollowByCustomerId(int customerId)
        {
            var key = string.Format(FOLLOWS_BY_CUSTOMERID, customerId, 0);
            return _cacheManager.GetCache(key).Get(key, () =>
            {
                return _followRepository.GetAllList(f => f.CustomerId == customerId);
            }).ToList();
        }

        public IPagedResult<CustomerFollow> GetFollows(int concernedId = 0, int customerId = 0,
            
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _followRepository.GetAll();
            if (concernedId > 0)
                query = query.Where(f => f.ConcernedCustomerId == concernedId);
            if (customerId > 0)
                query = query.Where(f => f.CustomerId == customerId);

            query = query.OrderByDescending(f => f.CreationTime);

            return new PagedResult<CustomerFollow>(query, pageIndex, pageSize);
        }

        public void InsertFollow(CustomerFollow follow)
        {
            if (follow == null)
                throw new ArgumentNullException("follow");

            _followRepository.Insert(follow);
            var key = string.Format(FOLLOWS_BY_CUSTOMERID, 0, follow.ConcernedCustomerId);
            _cacheManager.GetCache(key).Remove(key);
            key = string.Format(FOLLOWS_BY_CUSTOMERID, follow.CustomerId, 0);
            _cacheManager.GetCache(key).Remove(key);
            
        }

        #endregion
    }
}
