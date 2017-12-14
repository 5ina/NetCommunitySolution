using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using NetCommunitySolution.Domain.Catalog;

namespace NetCommunitySolution.Web.Models.Catalog
{

    [AutoMap(typeof(ContentLabel))]
    public class ContentLabelModel : EntityDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public bool Published { get; set; }

        
        public int DisplayOrder { get; set; }
        
    }
}