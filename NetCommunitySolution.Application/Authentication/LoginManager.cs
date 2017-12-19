using Abp.Application.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using NetCommunitySolution.Authentication.Dto;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NetCommunitySolution.Authentication
{

    public class LoginManager : SignInManager<CustomerDto, int>, IApplicationService
    {
        private readonly UserManager<CustomerDto, int> _userManager;
        private readonly IAuthenticationManager _authenticationManager;
        public LoginManager(UserManager<CustomerDto, int> userManager, AuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
            this._userManager = userManager;
            this._authenticationManager = authenticationManager;
        }

        public override Task SignInAsync(CustomerDto user, bool isPersistent, bool rememberBrowser)
        {
            return base.SignInAsync(user, isPersistent, rememberBrowser);
        }
        public override Task<ClaimsIdentity> CreateUserIdentityAsync(CustomerDto user)
        {
            return base.CreateUserIdentityAsync(user);
        }
    }
}
