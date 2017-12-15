using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using NetCommunitySolution.Domain.Catalog;
using System;

namespace NetCommunitySolution.Web.Models.Catalog
{
    [AutoMap(typeof(PostComment))]
    public class PostCommentModel : EntityDto
    {
        public int PostId { get; set; }

        public int CommentId { get; set; }

        public string Content { get; set; }

        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}