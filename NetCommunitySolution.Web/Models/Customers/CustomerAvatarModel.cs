using Abp.Application.Services.Dto;

namespace NetCommunitySolution.Web.Models.Customers
{
    public class CustomerAvatarModel:EntityDto
    {
        public string AvatarUrl { get; set; }

        public int MaxSize { get; set; }

        public string Exts { get; set; }

        public string Result { get; set; }
    }
}