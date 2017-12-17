using Abp.AutoMapper;
using NetCommunitySolution.Customers;
using NetCommunitySolution.Domain.Catalog;
using NetCommunitySolution.Web.Areas.Operate.Models.Customers;
using NetCommunitySolution.Web.Framework.Controllers;
using NetCommunitySolution.Web.Framework.Layui;
using NetCommunitySolution.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Areas.Operate.Controllers
{
    public class CustomerLevelController : AdminBaseController
    {
        #region Fields && Ctor

        private readonly ICustomerLevelService _levelService;

        public CustomerLevelController(ICustomerLevelService levelService)
        {
            this._levelService = levelService;
        }

        #endregion

        #region Method
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult List(DataSourceRequest command, string Keywords)
        {
            var levels = _levelService.GetAllLevels(keywords: Keywords,
                pageIndex: command.pageName - 1,
                pageSize: command.limitName);

            var data = new DataSourceResult
            {
                count = levels.TotalCount,
                data = levels.Items.Select(c => new
                {
                    LevelName = c.LevelName,
                    Id = c.Id,
                    DisplayOrder = c.DisplayOrder,
                    Level = c.Level
                }).ToList(),
            };
            return AbpJson(data);
        }

        public ActionResult Create()
        {
            var model = new CustomerLevelModel();
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(CustomerLevelModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var entity = model.MapTo<CustomerLevel>();

                var levelId = _levelService.InsertLevel(entity);

                if (continueEditing)
                {
                    return RedirectToAction("Edit", new { id = levelId });
                }
                return RedirectToAction("Index");
            }
            
            return View();
        }

        public ActionResult Edit(int id)
        {
            var entity = _levelService.GetCustomerLevelById(id);
            var model = entity.MapTo<CustomerLevelModel>();
            return View(model);
        }


        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(CustomerLevelModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var entity = _levelService.GetCustomerLevelById(model.Id);
                entity = model.MapTo<CustomerLevelModel, CustomerLevel>(entity);
                _levelService.UpdateLevel(entity);

                if (continueEditing)
                {
                    return RedirectToAction("Edit", new { id = model.Id });
                }
                return RedirectToAction("Index");
            }
            
            return View();
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            _levelService.DeleteLevel(id);
            return new NullJsonResult();
        }
        #endregion
    }
}