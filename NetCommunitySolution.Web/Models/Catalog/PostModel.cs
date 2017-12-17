using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using NetCommunitySolution.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Models.Catalog
{
    [AutoMap(typeof(Post))]
    public class PostModel : EntityDto
    {
        public PostModel()
        {
            AvailableCategories = new List<SelectListItem>();
        }

        [DisplayName("标题")]
        [Required, MaxLength(50)]
        public string Title { get; set; }

        [DisplayName("技术分类")]
        public int CategoryId { get; set; }

        [DisplayName("技术标签")]
        public int[] ContentLabelIds { get; set; }

        /// <summary>
        /// 是否解决
        /// </summary>
        public bool Solve { get; set; }
        /// <summary>
        /// 发布状态
        /// </summary>
        public bool Published { get; set; }

        public bool AllowComments { get; set; }

        [DisplayName("内容")]
        [UIHint("Editor")]
        public string Content { get; set; }

        public bool IsDeleted { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }


        public IList<SelectListItem> AvailableCategories { get; set; }
    }
}