using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using NetCommunitySolution.Domain.Catalog;
using System;
using System.ComponentModel;

namespace NetCommunitySolution.Web.Models.Catalog
{
    [AutoMap(typeof(Post))]
    public class SimplePostModel : EntityDto
    {
        
        public string Title { get; set; }

        [DisplayName("技术分类")]
        public int CategoryId { get; set; }


        /// <summary>
        /// 是否解决
        /// </summary>
        public bool Solve { get; set; }
        /// <summary>
        /// 发布状态
        /// </summary>
        public bool Published { get; set; }

        public bool AllowComments { get; set; }
        
        public string Description { get; set; }

        public bool IsDeleted { get; set; }
        public long? CreatorUserId { get; set; }

        public string CustomerName { get; set; }
        public DateTime CreationTime { get; set; }

        public int Answer { get; set; }

        public int Views { get; set; }

        public int Support { get; set; }
    }
}