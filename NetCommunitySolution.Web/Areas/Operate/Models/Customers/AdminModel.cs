using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using NetCommunitySolution.Domain.Customers;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Areas.Operate.Models.Customers
{
    [AutoMap(typeof(Customer))]
    public class AdminModel:EntityDto
    {
        [DataType(DataType.Password)]
        [DisplayName("密码")]
        [AllowHtml]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("确认密码")]
        [AllowHtml]
        public string ConfirmPassword { get; set; }
        
        [DisplayName("手机")]
        [AllowHtml]
        public string Phone { get; set; }

        
        [DisplayName("Email")]
        [AllowHtml]
        public string Email { get; set; }
        
        [DisplayName("登录名")]
        [AllowHtml]
        public string LoginName { get; set; }
        


    }
}