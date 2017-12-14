using Abp.Domain.Entities;

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
        public string Name { get; set; }

        public bool Published { get; set; }

        public int DisplayOrder { get; set; }
    }
}
