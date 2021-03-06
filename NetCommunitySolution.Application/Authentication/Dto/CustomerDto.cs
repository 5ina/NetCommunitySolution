﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Microsoft.AspNet.Identity;
using NetCommunitySolution.Domain.Customers;
using System;

namespace NetCommunitySolution.Authentication.Dto
{

    [AutoMap(typeof(Customer))]
    public class CustomerDto : EntityDto, IUser<int>
    {
        public string Mobile { get; set; }
        
        public string Email { get; set; }
        
        public string LoginName { get; set; }

        
        public string NickName { get; set; }

        /// <summary>
        /// 用户级别
        /// </summary>
        public int Level { get; set; }
        
        public string Password { get; set; }
        
        public string OpenId { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        public int CustomerRoleId { get; set; }
        
        public string PasswordSalt { get; set; }


        /// <summary>
        /// 最后修改时间（不需要处理）
        /// </summary>
        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// 创建时间（不需要处理）
        /// </summary>
        public DateTime CreationTime { get; set; }
        
        public string VerificationCode { get; set; }

        /// <summary>
        /// 验证码过期时间
        /// </summary>
        public DateTime VerificatExpiryTime { get; set; }

        public string UserName
        {
            get { return LoginName; }
            set { this.LoginName = value; }
        }
    }
}
