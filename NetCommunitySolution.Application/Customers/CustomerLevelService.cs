using System;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using NetCommunitySolution.Domain.Catalog;

namespace NetCommunitySolution.Customers
{
    public class CustomerLevelService : NetCommunitySolutionAppServiceBase, ICustomerLevelService
    {

        #region Ctor && Field

        /// <summary>
        /// 用户缓存key，{0} Level ID
        /// </summary>
        private const string LEVELS_BY_ID = "net.customerlevels.by-id.{0}";


        private const string LEVELS_KEYS = "net.customerlevels.all";


        private readonly IRepository<CustomerLevel> _levelRepository;
        private readonly ICacheManager _cacheManager;
        public CustomerLevelService(
            IRepository<CustomerLevel> levelRepository,
            ICacheManager cacheManager)
        {
            this._levelRepository = levelRepository;
            this._cacheManager = cacheManager;
        }

        #endregion
        #region Method
        public void DeleteLevel(int levelId)
        {
            if (levelId > 0)
                _levelRepository.Delete(levelId);
            string key = string.Format(LEVELS_BY_ID, levelId);
            _cacheManager.GetCache(key).Remove(key);
            _cacheManager.GetCache(LEVELS_KEYS).Remove(LEVELS_KEYS);
        }

        public IPagedResult<CustomerLevel> GetAllLevels(string keywords = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _cacheManager.GetCache(LEVELS_KEYS).Get(LEVELS_KEYS, () => {

               var queryable = _levelRepository.GetAll();
                return queryable;
            });
            if (!String.IsNullOrWhiteSpace(keywords))
                query = query.Where(l => l.LevelName.Contains(keywords));

            query = query.OrderByDescending(l => l.Level);

            return new PagedResult<CustomerLevel>(query, pageIndex, pageSize);
        }

        public CustomerLevel GetCustomerLevelById(int levelId)
        {
            if (levelId == 0)
                return null;
            string key = string.Format(LEVELS_BY_ID, levelId);
            return _cacheManager.GetCache(key).Get(key, () => {
                return _levelRepository.Get(levelId);
            });
        }

        public int InsertLevel(CustomerLevel level)
        {
            if (level == null)
                throw new ArgumentNullException("level");

            var levelId = _levelRepository.InsertAndGetId(level);

            //cache
            _cacheManager.GetCache(LEVELS_KEYS).Remove(LEVELS_KEYS);

            return levelId;
        }

        public void UpdateLevel(CustomerLevel level)
        {
            if (level == null)
                throw new ArgumentNullException("level");

            _levelRepository.Update(level);

            //cache
            _cacheManager.GetCache(LEVELS_KEYS).Remove(LEVELS_KEYS);
            
        }

        #endregion
    }
}
