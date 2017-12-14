using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using NetCommunitySolution.Domain.Catalog;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Areas.Operate.Models.Catalog
{
    [AutoMap(typeof(ContentLabel))]
    public class ContentLabelModel:EntityDto
    {
        public ContentLabelModel()
        {
            SelectedCategoryIds = new List<int>();
            AvailableCategories = new List<SelectListItem>();
        }

        [DisplayName("类目名称")]
        public int CategoryId { get; set; }
        /// <summary>
        /// 内容标签名称
        /// </summary>
        [DisplayName("标签名称")]
        public string Name { get; set; }

        [DisplayName("是否发布")]
        public bool Published { get; set; }


        [DisplayName("权重")]
        public int DisplayOrder { get; set; }

        
        [UIHint("SingleSelect")]
        public IList<int> SelectedCategoryIds { get; set; }

        public IList<SelectListItem> AvailableCategories { get; set; }
    }
}