using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using NetCommunitySolution.Domain.Catalog;
using System;
using System.Collections.Generic;

namespace NetCommunitySolution.Web.Models.Catalog
{

    [AutoMap(typeof(Post))]
    public class PostDetailModel : EntityDto
    {
        public PostDetailModel()
        {
            this.ContentLabels = new List<ContentLabelModel>();
        }

        public string Title { get; set; }
        
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }


        public IList<ContentLabelModel> ContentLabels { get; set; }

        /// <summary>
        /// 发布状态
        /// </summary>
        public bool Published { get; set; }

        public bool AllowComments { get; set; }
        
        public string Content { get; set; }

        public bool IsDeleted { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }                
    }
}