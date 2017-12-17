using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using NetCommunitySolution.Domain.Catalog;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NetCommunitySolution.Web.Areas.Operate.Models.Customers
{
    [AutoMap(typeof(CustomerLevel))]
    public class CustomerLevelModel : EntityDto
    {
        /// <summary>
        /// 用户的级别名称
        /// </summary>
        [DisplayName("名称")]
        [Required, MaxLength(10)]
        public string LevelName { get; set; }

        [DisplayName("级别")]

        public int Level { get; set; }
        [DisplayName("权重")]
        public int DisplayOrder { get; set; }
    }
}