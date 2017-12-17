using System.Collections.Generic;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using NetCommunitySolution.Domain.Customers;

namespace NetCommunitySolution.Customers
{
    public class CustomerAttributeService : NetCommunitySolutionAppServiceBase,ICustomerAttributeService
    {

        #region Ctor && Field
        /// <summary>
        /// 用户属性{0} 用户主键
        /// </summary>
        private const string CUSTOMER_ATTRIBUTES_BY_CUSTOMERID = "net.customerattributes.bycustomerid-{0}";

        private readonly IRepository<CustomerAttribute> _attributeRepository;
        private readonly ICacheManager _cacheManager;

        public CustomerAttributeService(IRepository<CustomerAttribute> attributeRepository,
            ICacheManager cacheManager)
        {
            this._attributeRepository = attributeRepository;
            this._cacheManager = cacheManager;
        }


        #endregion

        #region Method
        public void ClearAttribute(int customerId)
        {
            _attributeRepository.Delete(x => x.CustomerId == customerId);
            var key = string.Format(CUSTOMER_ATTRIBUTES_BY_CUSTOMERID, customerId);
            _cacheManager.GetCache(key).Remove(customerId.ToString());
        }

        public void DeleteAttribute(int attributeId)
        {
            if (attributeId > 0)
                _attributeRepository.Delete(attributeId);
        }

        public IList<CustomerAttribute> GetAttributeByCustomerId(int customerId)
        {
            var key = string.Format(CUSTOMER_ATTRIBUTES_BY_CUSTOMERID, customerId);
            return _cacheManager.GetCache(key).Get(customerId.ToString(), () =>
            {
                return _attributeRepository.GetAllList(x => x.CustomerId == customerId);
            });            
        }

        public void SaveAttribute(CustomerAttribute attribute)
        {
            if (attribute == null || attribute.CustomerId <= 0)
                return;

            if (attribute.Id == 0)
                _attributeRepository.Insert(attribute);
            else
                _attributeRepository.Update(attribute);
            var key = string.Format(CUSTOMER_ATTRIBUTES_BY_CUSTOMERID, attribute.CustomerId);
            _cacheManager.GetCache(key).Remove(attribute.CustomerId.ToString());
            
        }
        #endregion
    }
}
