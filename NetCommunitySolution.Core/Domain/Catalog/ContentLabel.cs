﻿using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace NetCommunitySolution.Domain.Catalog
{
    /// <summary>
    /// 内容标签
    /// </summary>
    public class ContentLabel : Entity
    {
        public int CategoryId { get; set; }
        /// <summary>
        /// 内容标签名称
        /// </summary>
        [Required, MaxLength(50)]
        public string Name { get; set; }

        public bool Published { get; set; }

        public int DisplayOrder { get; set; }
    }
}
