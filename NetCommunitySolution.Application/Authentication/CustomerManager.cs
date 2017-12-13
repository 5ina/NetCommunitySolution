using Abp.Application.Services;
using Microsoft.AspNet.Identity;
using NetCommunitySolution.Authentication.Dto;

namespace NetCommunitySolution.Authentication
{

    public class CustomerManager : UserManager<CustomerDto, int>, IApplicationService
    {
        public CustomerManager(CustomerStore store) : base(store)
        {
        }
    }
}
