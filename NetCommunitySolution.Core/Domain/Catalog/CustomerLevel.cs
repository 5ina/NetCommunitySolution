using Abp.Domain.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NetCommunitySolution.Domain.Catalog
{
    /// <summary>
    /// 用户级别
    /// </summary>
    public class CustomerLevel : Entity
    {
        /// <summary>
        /// 用户的级别名称
        /// </summary>
        [Required, MaxLength(10)]
        public string LevelName { get; set; }
        

        public int Level { get; set; }
        public int DisplayOrder { get; set; }
    }
}
