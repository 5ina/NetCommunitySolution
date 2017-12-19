using Abp.AutoMapper;
using NetCommunitySolution.Customers;
using NetCommunitySolution.Domain.Customers;
using NetCommunitySolution.Web.Areas.Operate.Models.Customers;
using NetCommunitySolution.Web.Framework.Controllers;
using NetCommunitySolution.Web.Framework.Html;
using NetCommunitySolution.Web.Framework.Layui;
using NetCommunitySolution.Web.Framework.Mvc;
using System;
using System.Linq;
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

        public ActionResult Create()
        {
            var model = new AdminModel();
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(AdminModel model, bool continueEditing)
        {
            if (!model.Password.Equals(model.ConfirmPassword))
            {
                ModelState.AddModelError("", "两次密码输入的不相同");
            }

            if (ModelState.IsValid)
            {
                var entity = model.MapTo<Customer>();

                var customerId = _customerService.CreateCustomer(entity);

                if (continueEditing)
                {
                    return RedirectToAction("Edit", new { id = customerId });
                }
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var entity = _customerService.GetCustomerId(id);
            if (entity != null)
            {
                var model = entity.MapTo<AdminModel>();
                return View(model);
            }
            return RedirectToAction("List");
        }


        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(AdminModel model, bool continueEditing)
        {
            if (!model.Password.Equals(model.ConfirmPassword))
            {
                ModelState.AddModelError("", "两次密码输入的不相同");
            }

            if (ModelState.IsValid)
            {
                var entity = _customerService.GetCustomerId(model.Id);
                entity = model.MapTo<AdminModel, Customer>(entity);
                _customerService.UpdateCustomer(entity);

                if (continueEditing)
                {
                    return RedirectToAction("Edit", new { id = model.Id });
                }
                return RedirectToAction("Index");
            }

            return View(model);
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            _customerService.DeleteCustomer(id);
            return new NullJsonResult();
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