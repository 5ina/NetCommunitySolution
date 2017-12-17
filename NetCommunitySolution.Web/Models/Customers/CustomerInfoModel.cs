using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using NetCommunitySolution.Domain.Customers;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NetCommunitySolution.Web.Models.Customers
{
    [AutoMap(typeof(Customer))]
    public class CustomerInfoModel :EntityDto
    {

        /// <summary>
        /// 手机号
        /// </summary>
        [MaxLength(15)]
        [DisplayName("手机")]
        public string Mobile { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [MaxLength(50)]
        [DisplayName("Email")]
        public string Email { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        [MaxLength(30)]
        [DisplayName("登录名")]
        public string LoginName { get; set; }


        /// <summary>
        /// 昵称
        /// </summary>
        [MaxLength(30)]
        [DisplayName("昵称")]
        public string NickName { get; set; }

        /// <summary>
        /// 用户级别
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required, MaxLength(60)]
        public string Password { get; set; }
        
                

        /// <summary>
        /// 最后修改时间（不需要处理）
        /// </summary>
        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// 创建时间（不需要处理）
        /// </summary>
        public DateTime CreationTime { get; set; }


        [MaxLength(30)]
        [DisplayName("QQ")]
        public string QQ { get; set; }

        public bool EnabledModifyName { get; set; }

        public string Result { get; set; }

    }
}