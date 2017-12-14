using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using NetCommunitySolution.Domain.Catalog;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Areas.Operate.Models.Catalog
{
    [AutoMap(typeof(Category))]
    public class CategoryModel : EntityDto
    {
        public CategoryModel()
        {
            SelectedCategoryIds = new List<int>();
            AvailableCategories = new List<SelectListItem>();
        }
        /// <summary>
        /// 类目名称
        /// </summary>
        [DisplayName("名称")]
        public string Name { get; set; }


        /// <summary>
        /// 介绍
        /// </summary>
        [DisplayName("介绍")]
        public string Description { get; set; }

        /// <summary>
        /// 类目模板
        /// </summary>
        [DisplayName("模板名称")]
        public string CategoryTemplateNames { get; set; }



        [DisplayName("MetaKeywords")]
        public string MetaKeywords { get; set; }

        [DisplayName("MetaDescription")]
        public string MetaDescription { get; set; }

        [DisplayName("MetaTitle")]
        public string MetaTitle { get; set; }

        /// <summary>
        /// 父类别
        /// </summary>
        [DisplayName("父类别")]
        public int ParentCategoryId { get; set; }

        /// <summary>
        /// 是否显示在顶部菜单
        /// </summary>
        [DisplayName("首页显示")]
        public bool IncludeInTopMenu { get; set; }

        /// <summary>
        /// 是否发布
        /// </summary>
        [DisplayName("是否发布")]
        public bool Published { get; set; }

        /// <summary>
        /// 权重
        /// </summary>
        [DisplayName("权重")]
        public int DisplayOrder { get; set; }
        [UIHint("SingleSelect")]
        public IList<int> SelectedCategoryIds { get; set; }

        public IList<SelectListItem> AvailableCategories { get; set; }
    }
}