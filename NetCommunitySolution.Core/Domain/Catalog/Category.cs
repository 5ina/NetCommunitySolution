using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace NetCommunitySolution.Domain.Catalog
{
    public class Category : Entity
    {
        /// <summary>
        /// 类目名称
        /// </summary>
        [Required, MaxLength(15)]
        public string Name { get; set; }


        /// <summary>
        /// 介绍
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 类目模板
        /// </summary>
        [MaxLength(15)]
        public string CategoryTemplateNames { get; set; }



        [MaxLength(200)]
        public string MetaKeywords { get; set; }

        [MaxLength(200)]
        public string MetaDescription { get; set; }

        [MaxLength(200)]
        public string MetaTitle { get; set; }

        /// <summary>
        /// 父类别
        /// </summary>
        public int ParentCategoryId { get; set; }

        /// <summary>
        /// 是否显示在顶部菜单
        /// </summary>
        public bool IncludeInTopMenu { get; set; }

        /// <summary>
        /// 是否发布
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// 权重
        /// </summary>
        public int DisplayOrder { get; set; }
    }
}
