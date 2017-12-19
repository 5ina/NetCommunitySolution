using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using NetCommunitySolution.Domain.Customers;

namespace NetCommunitySolution.Customers
{
    public class RewardPointService : NetCommunitySolutionAppServiceBase, IRewardPointService
    {

        #region Fields
        private readonly IRepository<RewardPointsHistory> _historyRepository;
        private readonly ICacheManager _cacheManager;


        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : category ID
        /// </remarks>
        private const string REWARD_BY_ID_KEY = "net.reward.id-{0}";


        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : customer ID
        /// </remarks>
        private const string REWARD_BY_CUSTOMERID = "net.reward.bycustomerid-{0}";

        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string REWARD_PATTERN_KEY = "net.reward.";

        #endregion

        #region Ctor


        public RewardPointService(IRepository<RewardPointsHistory> historyRepository, ICacheManager cacheManager)
        {
            this._historyRepository = historyRepository;
            this._cacheManager = cacheManager;
        }
        #endregion
        #region Method

        public IList<RewardPointsHistory> GetPointsHistoryByCustomerId(int customerId = 0)
        {
            var key = string.Format(REWARD_BY_CUSTOMERID, customerId);
            return _cacheManager.GetCache(key).Get(key, () => {
                var query = from r in _historyRepository.GetAll()
                            where r.CustomerId == customerId
                            orderby r.CreationTime descending
                            select r;
                return query.ToList();
            });
        }

        public void DeletePoint(int pointId)
        {
            _historyRepository.Delete(pointId);
            _cacheManager.RemoveByPattern(REWARD_PATTERN_KEY);
        }

        public IPagedResult<RewardPointsHistory> GetAllPointsHistory(string keywords = "",
            DateTime? createdFrom = null, DateTime? createdTo = null, 
            int customerId = 0,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _historyRepository.GetAll();

            if (createdFrom.HasValue)
                query = query.Where(h => createdFrom.Value <= h.CreationTime);
            if (createdTo.HasValue)
                query = query.Where(h => createdTo.Value >= h.CreationTime);

            if (customerId > 0)
                query = query.Where(h => h.CustomerId == customerId);

            if (!String.IsNullOrWhiteSpace(keywords))
                query = query.Where(h => h.Message.Contains(keywords));

            query = query.OrderBy(h => h.CustomerId).ThenByDescending(h => h.CreationTime);

            return new PagedResult<RewardPointsHistory>(query, pageIndex, pageSize);
        }

        public RewardPointsHistory GetPointsHistoryById(int pointId)
        {
            if (pointId <= 0)
                return null;

            var key = string.Format(REWARD_BY_ID_KEY, pointId);
            return _cacheManager.GetCache(key).Get(key, () =>
            {
                return _historyRepository.Get(pointId);
            });
        }

        public void InsertPoint(RewardPointsHistory point)
        {
            if (point == null)
                throw new ArgumentNullException("pointHistory");

            _historyRepository.Insert(point);

            //cache
            _cacheManager.RemoveByPattern(REWARD_PATTERN_KEY);
            
        }

        public void UpdatePoint(RewardPointsHistory point)
        {
            if (point == null)
                throw new ArgumentNullException("pointHistory");

            _historyRepository.Update(point);

            //cache
            _cacheManager.RemoveByPattern(REWARD_PATTERN_KEY);
        }
        #endregion
    }
}
