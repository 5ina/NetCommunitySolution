using Abp.Domain.Entities;

namespace NetCommunitySolution.Domain.Catalog
{
    /// <summary>
    /// 帖子的标签
    /// </summary>
    public class PostLabel :Entity
    {
        public int PostId { get; set; }

        public int LabelId { get; set; }
    }
}
