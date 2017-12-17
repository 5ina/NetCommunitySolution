using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using NetCommunitySolution.Domain.Catalog;

namespace NetCommunitySolution.Web.Models.Catalog
{
    [AutoMap(typeof(Category))]
    public class CategoryModel:EntityDto
    {
        public string Name { get; set; }

        
        public string Description { get; set; }
        
        public string CategoryTemplateNames { get; set; }
        
        public string MetaKeywords { get; set; }
        
        public string MetaDescription { get; set; }
        
        public string MetaTitle { get; set; }
        
        public int ParentCategoryId { get; set; }
        
        public bool IncludeInTopMenu { get; set; }
        
        public bool Published { get; set; }
        
        public int DisplayOrder { get; set; }
    }

    public class CategoryPostModel
    {
        public CategoryPostModel()
        {
            this.Category = new CategoryModel();
            this.Post = new PagedSimplePostModel();
        }

        public CategoryModel Category { get; set; }
        public PagedSimplePostModel Post { get; set; }
    }
}