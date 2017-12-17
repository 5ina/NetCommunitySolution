using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace NetCommunitySolution.Domain.Catalog
{
    /// <summary>
    /// 帖子
    /// </summary>
    public class Post : Entity, ISoftDelete, ICreationAudited, IModificationAudited
    {
        [Required, MaxLength(50)]
        public string Title { get; set; }

        public int CategoryId { get; set; }

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

        public int Answer { get; set; }
        /// <summary>
        /// 是否解决
        /// </summary>
        public bool Solve { get; set; }

        public int Views { get; set; }

        public int Support { get; set; }
    }
}
