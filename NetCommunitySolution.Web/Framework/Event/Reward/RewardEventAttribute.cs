using Abp.Dependency;
using Abp.Runtime.Session;
using NetCommunitySolution.Common;
using NetCommunitySolution.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin;


namespace NetCommunitySolution.Web.Framework.Event.Reward
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class RewardEventAttribute : ActionFilterAttribute, ITransientDependency
    {
        private readonly RewardEventMode _mode;
        public RewardEventAttribute(RewardEventMode mode)
        {
            this._mode = mode;
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var AbpSession = Abp.Dependency.IocManager.Instance.Resolve<IAbpSession>();
            var s = filterContext.HttpContext.GetOwinContext().Authentication;
            var user = s.User.Identity;
            var customerId = Convert.ToInt32(AbpSession.UserId);
            var customerService = Abp.Dependency.IocManager.Instance.Resolve<ICustomerService>();
            var settingService = Abp.Dependency.IocManager.Instance.Resolve<ISettingService>();
            var rewardSetting = settingService.GetRewardSettings();
            var customer = customerService.GetCustomerId(customerId);
            var point = 0;
            var message = "";
            switch (_mode)
            {
                case RewardEventMode.Comment:
                    point = rewardSetting.Comment;
                    message = "发布评论获取积分";
                    break;
                case RewardEventMode.NewPost:
                    point = rewardSetting.NewPost;
                    message = "发帖获取积分";
                    break;
                case RewardEventMode.Selected:
                    point = rewardSetting.Selected;
                    message = "您的回复获得选定获取积分";
                    customerId = Convert.ToInt32(filterContext.HttpContext.Request.Form["selectedCustomerId"]);
                    customer = customerService.GetCustomerId(customerId);
                    break;
                case RewardEventMode.Solve:
                    point = rewardSetting.Solve;
                    message = "结贴获取积分";
                    break;
            }

            customer.InsertCustomerRewardHistroy(point, Domain.Customers.RewardPointMode.Get, message, rewardSetting.DayMaxReward);            
            base.OnResultExecuted(filterContext);
        }
        
    }
}