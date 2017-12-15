using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;

namespace NetCommunitySolution.Domain.Catalog
{
    public class PostComment : Entity, ICreationAudited, IModificationAudited
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