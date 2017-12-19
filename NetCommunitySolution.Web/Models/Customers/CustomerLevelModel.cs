using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using NetCommunitySolution.Domain.Catalog;
using System.Collections.Generic;

namespace NetCommunitySolution.Web.Models.Customers
{

    public class MyLevelModel
    {
        public MyLevelModel()
        {
            this.MyLevel = new CustomerLevelModel();
            this.Levels = new List<CustomerLevelModel>();
        }

        public CustomerLevelModel MyLevel { get; set; }

        public IList<CustomerLevelModel> Levels { get; set; }

    }

    [AutoMap(typeof(CustomerLevel))]
    public class CustomerLevelModel : EntityDto
    {
        /// <summary>
        /// 用户的级别名称
        /// </summary>
        public string LevelName { get; set; }
        

        public int Level { get; set; }
        public int DisplayOrder { get; set; }
    }
}