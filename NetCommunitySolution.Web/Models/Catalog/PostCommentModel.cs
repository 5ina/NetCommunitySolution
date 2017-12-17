using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using NetCommunitySolution.Domain.Catalog;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NetCommunitySolution.Web.Models.Catalog
{
    [AutoMap(typeof(PostComment))]
    public class PostCommentModel : EntityDto
    {
        public int PostId { get; set; }

        public int CommentId { get; set; }

        [DisplayName("内容")]
        [UIHint("Editor")]
        public string Content { get; set; }

        /// <summary>
        /// 是否选定
        /// </summary>
        public bool Selected { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}