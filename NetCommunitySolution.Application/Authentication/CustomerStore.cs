using Abp.AutoMapper;
using Microsoft.AspNet.Identity;
using NetCommunitySolution.Authentication.Dto;
using NetCommunitySolution.Customers;
using NetCommunitySolution.Domain.Customers;
using System;
using System.Threading.Tasks;

namespace NetCommunitySolution.Authentication
{

    public class CustomerStore : IUserStore<CustomerDto, int>
    {
        public CustomerStore(ICustomerService customerService)
        {
            this._customerService = customerService;
        }
        private readonly ICustomerService _customerService;

        public Task CreateAsync(CustomerDto user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(CustomerDto user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<CustomerDto> FindByIdAsync(int customerId)
        {
            var customer = await _customerService.GetCustomerIdAsync(customerId);

            return customer.MapTo<CustomerDto>();
        }

        public async Task<CustomerDto> FindByNameAsync(string loginName)
        {
            var customer = await _customerService.GetCustomerByMobileAsync(loginName);
            return customer.MapTo<CustomerDto>();
        }

        public async Task UpdateAsync(CustomerDto user)
        {
            var customer = user.MapTo<Customer>();

            await _customerService.UpdateAsyncCustomer(customer);
        }
    }
}
