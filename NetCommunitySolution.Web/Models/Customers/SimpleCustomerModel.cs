using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using NetCommunitySolution.Domain.Customers;
using System.ComponentModel.DataAnnotations;

namespace NetCommunitySolution.Web.Models.Customers
{
    [AutoMap(typeof(Customer))]
    public class SimpleCustomerModel:EntityDto
    {/// <summary>
     /// 手机号
     /// </summary>
        [MaxLength(15)]
        public string Mobile { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [MaxLength(50)]
        public string Email { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        [MaxLength(30)]
        public string LoginName { get; set; }


        /// <summary>
        /// 昵称
        /// </summary>
        [MaxLength(30)]
        public string NickName { get; set; }

        /// <summary>
        /// 用户级别
        /// </summary>
        public int Level { get; set; }

        public CustomerRole CustomerRole { get; set; }

        /// <summary>
        /// 网站名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 副标题
        /// </summary>
        public string SubTitle { get; set; }

    }
}