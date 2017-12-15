using Abp.Application.Services.Dto;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Models.Customers
{
    public partial class ChangePasswordModel :EntityDto
    {
        public string LoginName { get; set; }

        [AllowHtml]
        [DisplayName("原密码")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [AllowHtml]
        [DataType(DataType.Password)]
        [DisplayName("新密码")]
        public string NewPassword { get; set; }

        [AllowHtml]
        [DataType(DataType.Password)]
        [DisplayName("确认新密码")]
        public string ConfirmNewPassword { get; set; }

        public string Result { get; set; }

    }
}