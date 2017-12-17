using Abp.Domain.Repositories;
using System;
using Abp.Application.Services.Dto;
using System.Linq;
using System.Threading.Tasks;
using Abp.Notifications;
using Abp.Runtime.Caching;
using NetCommunitySolution.Authentication.Dto;
using NetCommunitySolution.Domain.Customers;
using System.Linq.Dynamic.Core;
using NetCommunitySolution.Security;

namespace NetCommunitySolution.Customers
{
    public class CustomerService : NetCommunitySolutionAppServiceBase, ICustomerService
    {
        #region Ctor && Field

        /// <summary>
        /// 用户缓存key，{0}为用户ID
        /// </summary>
        private const string CUSTOMER_BY_ID = "net.customer.by-id.{0}";


        private readonly IRepository<Customer> _customerRepository;
        private readonly ICacheManager _cacheManager;
        private readonly IEncryptionService _encryptionService;
        private readonly INotificationSubscriptionManager _notificationSubscriptionManager;
        public CustomerService(
            IRepository<Customer> customerRepository,
            ICacheManager cacheManager,
            IEncryptionService encryptionService,
            INotificationSubscriptionManager notificationSubscriptionManager)
        {
            this._customerRepository = customerRepository;
            this._cacheManager = cacheManager;
            this._encryptionService = encryptionService;
            this._notificationSubscriptionManager = notificationSubscriptionManager;
        }

        #endregion

        #region Method        
        public int CreateCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            return _customerRepository.InsertAndGetId(customer);

        }

        public void DeleteCustomer(int customerId)
        {
            _customerRepository.Delete(customerId);
        }

        public IPagedResult<Customer> GetAllCustomers(DateTime? createdFrom = null,
            DateTime? createdTo = null,
            bool? isSub = null,
            CustomerRole? roleId = null,
            string keywords = null,
            bool showHidden = false, 
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _customerRepository.GetAll();
                        
            if (createdFrom.HasValue)
                query = query.Where(c => createdFrom.Value <= c.CreationTime);
            if (createdTo.HasValue)
                query = query.Where(c => createdTo.Value >= c.CreationTime);
            

            if (!String.IsNullOrWhiteSpace(keywords))
                query = query.Where(c => c.NickName.Contains(keywords) || c.Mobile.Contains(keywords));

            if (roleId.HasValue)
                query = query.Where(c => c.CustomerRoleId == (int)roleId.Value);
            

            query = query.OrderByDescending(c => c.CreationTime);

            return new PagedResult<Customer>(query, pageIndex, pageSize);
        }

        public Customer GetCustomerByMobile(string mobile)
        {
            return _customerRepository.GetAllList(x => x.Mobile == mobile).FirstOrDefault();
        }

        public async Task<Customer> GetCustomerByMobileAsync(string mobile)
        {
            return await _customerRepository.FirstOrDefaultAsync(c => c.Mobile == mobile);
        }
        public Customer GetCustomerId(int customerId)
        {
            if (customerId > 0)
            {
                var key = string.Format(CUSTOMER_BY_ID, customerId);
                return _cacheManager.GetCache(key).Get(customerId.ToString(), () =>
                {
                    return _customerRepository.Get(customerId);
                });
            }
            else
                return null;
        }

        public async Task<Customer> GetCustomerIdAsync(int customerId)
        {
            var key = string.Format(CUSTOMER_BY_ID, customerId);
            return await _cacheManager.GetCache(key).GetAsync(customerId.ToString(), () =>
            {
                return _customerRepository.GetAsync(customerId);
            });
        }


        public void UpdateCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            _customerRepository.Update(customer);
            if (customer.Id > 0)
                _cacheManager.GetCache(string.Format(CUSTOMER_BY_ID, customer.Id)).Remove(customer.Id.ToString());            
        }

        public async Task UpdateAsyncCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            await _customerRepository.UpdateAsync(customer);
            await _cacheManager.GetCache(string.Format(CUSTOMER_BY_ID, customer.Id)).RemoveAsync(customer.Id.ToString());
            
        }

        public CustomerLoginResults ValidateCustomer(string loginName, string password)
        {
            var result = new CustomerLoginResults();
            result.Result = LoginResults.WrongPassword;
            var customer = GetCustomerByMobile(loginName);
            if (customer == null)
                return new CustomerLoginResults(LoginResults.NotRegistered);
            var psd = _encryptionService.CreatePasswordHash(password, customer.PasswordSalt);
            bool isValid = psd == customer.Password;
            if (isValid)
                result.Result = LoginResults.Successful;

            customer.LastModificationTime = DateTime.Now;
            //_customerService.UpdateCustomer(customer);
            result.Customer = customer;
            return result;
        }

        public async Task<CustomerLoginResults> ValidateAsyncCustomer(string loginName, string password)
        {
            var result = new CustomerLoginResults();
            var customer = await GetCustomerByMobileAsync(loginName);
            if (customer == null)
                return new CustomerLoginResults(LoginResults.NotRegistered);
            var psd = _encryptionService.CreatePasswordHash(password, customer.PasswordSalt);
            bool isValid = psd == customer.Password;
            if (!isValid)
                result.Result = LoginResults.WrongPassword;
            customer.LastModificationTime = DateTime.Now;
            //_customerService.UpdateCustomer(customer);
            result.Result = LoginResults.Successful;
            result.Customer = customer;
            return result;
        }

        public Customer GetCustomerByOpenId(string openId)
        {
            return _customerRepository.FirstOrDefault(c => c.OpenId == openId);
            //var key = string.Format(CUSTOMER_BY_OPENID, openId);
            //return _cacheManager.GetCache(key).Get(openId, () =>
            //{
            //var customer = _customerRepository.GetAllList(x => x.OpenId == openId).FirstOrDefault();
            //if (customer == null)
            //{
            //    customer = new Customer();
            //    var code = CommonHelper.GenerateCode(6);
            //    customer.PasswordSalt = code;
            //    customer = new Customer
            //    {
            //        CustomerRole = CustomerRole.Buyer,
            //        OpenId = openId,
            //        PasswordSalt = code,
            //        Password = _encryptionService.CreatePasswordHash("123456", code)
            //    };

            //    customer.Id = _customerRepository.InsertAndGetId(customer);
            //}

            //return customer;
            //});
        }

        public bool HasCustomer(string openId ,out Customer customer)
        {
            customer = _customerRepository.FirstOrDefault(e => e.OpenId == openId);
            if (customer == null)
                return false;
            if (customer.Id == 0)
                return false;
            return true;
        }

        public int GetRegisteredCustomersReport(int days)
        {
            DateTime date = DateTime.Now.AddDays(-days);

            var query = from c in _customerRepository.GetAll()
                        where
                        c.CustomerRoleId == (int)CustomerRole.Member &&
                        c.CreationTime >= date 
                        //&& c.CreatedOnUtc <= DateTime.UtcNow
                        select c;
            int count = query.Count();
            return count;
        }

        #endregion
    }
}
