using Abp.Application.Services.Dto;

namespace NetCommunitySolution.Web.Areas.Operate.Models.Customers
{
    public class CustomerBoxModel :EntityDto
    {
        public string NikeName { get; set; }

        public string Avatar { get; set; }
    }
}