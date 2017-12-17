using NetCommunitySolution.Customers;
using NetCommunitySolution.Domain.Customers;
using NetCommunitySolution.Web.Areas.Operate.Models.Customers;
using NetCommunitySolution.Web.Framework.Html;
using NetCommunitySolution.Web.Framework.Layui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Areas.Operate.Controllers
{
    public class CustomerController : AdminBaseController
    {
        #region Fields && Ctor

        private readonly ICustomerService _customerService;
        private readonly ICustomerLevelService _levelService;

        public CustomerController(ICustomerService customerService, ICustomerLevelService levelService)
        {
            this._customerService = customerService;
            this._levelService = levelService;
        }

        #endregion

        #region method
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult List(DataSourceRequest command, string Keywords)
        {
            var customers = _customerService.GetAllCustomers(keywords: Keywords,
                pageIndex: command.pageName - 1,
                pageSize: command.limitName);

            var data = new DataSourceResult
            {
                count = customers.TotalCount,
                data = customers.Items.Select(c =>
                {
                    var level = _levelService.GetCustomerLevelById(c.Level);
                    var customerRole = (CustomerRole)c.CustomerRoleId;
                    var model = new
                    {
                        NickName = c.NickName,
                        Id = c.Id,
                        Mobile = c.Mobile,
                        Email = c.Email,
                        Level = string.Format("{0}({1})", level.LevelName, level.Level),
                        CustomerRole = customerRole.GetDescription(),
                        CreateTime = c.CreationTime.ToString("yyyy.MM.dd HH:mm"),
                    };
                    return model;
                }).ToList(),
            };
            return AbpJson(data);
        }
        #endregion

        #region Child Action
        [ChildActionOnly]
        public ActionResult CustomerHeader()
        {
            var customer = _customerService.GetCustomerId(Convert.ToInt32(AbpSession.UserId));
            var model = new CustomerBoxModel();

            model.Id = customer.Id;
            model.Avatar = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.Avatar);
            model.NikeName = customer.NickName;
            return PartialView(model);

        }
        #endregion
    }
}