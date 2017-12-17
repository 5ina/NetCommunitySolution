using Abp.AutoMapper;
using Abp.Runtime.Caching;
using NetCommunitySolution.Catalog;
using NetCommunitySolution.Domain.Catalog;
using NetCommunitySolution.Web.Areas.Operate.Models.Catalog;
using NetCommunitySolution.Web.Framework.Controllers;
using NetCommunitySolution.Web.Framework.Html;
using NetCommunitySolution.Web.Framework.Layui;
using NetCommunitySolution.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Areas.Operate.Controllers
{
    public class ContentLabelController : AdminBaseController
    {
        #region Fields && Ctor

        private readonly IContentLabelService _labelService;
        protected readonly ICategoryService _categoryService;
        private readonly ICacheManager _cacheManager;

        public ContentLabelController(IContentLabelService labelService, 
            ICategoryService categoryService,
            ICacheManager cacheManager)
        {
            this._labelService = labelService;
            this._categoryService = categoryService;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Utilities
        [NonAction]
        protected virtual void PrepareContentLabelModel(ContentLabelModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            
            var allCategories = SelectListHelper.GetCategoryList(_categoryService, _cacheManager, true);
            foreach (var c in allCategories)
            {
                c.Selected = model.SelectedCategoryIds.Contains(int.Parse(c.Value));
                model.AvailableCategories.Add(c);
            }
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
            var labels = _labelService.GetAllLabels(keywords: Keywords,
                pageIndex: command.pageName - 1,
                pageSize: command.limitName);

            var data = new DataSourceResult
            {
                count = labels.TotalCount,
                data = labels.Items.Select(c => new
                {
                    Name = c.Name,
                    Id = c.Id,
                    DisplayOrder = c.DisplayOrder
                }).ToList(),
            };
            return AbpJson(data);
        }

        public ActionResult Create()
        {
            var model = new ContentLabelModel();
            PrepareContentLabelModel(model);
            model.DisplayOrder = 999;
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(ContentLabelModel model, bool continueEditing)
        {
            if (ModelState.IsValid) {
                var entity = model.MapTo<ContentLabel>();

                var labelId = _labelService.InsertContentLabel(entity);

                if (continueEditing)
                {
                    return RedirectToAction("Edit", new { id = labelId });
                }
                return RedirectToAction("Index");
            }

            PrepareContentLabelModel(model);
            return View();
        }


        public ActionResult Edit(int id)
        {
            var entity = _labelService.GetContentLabelById(id);
            var model = entity.MapTo<ContentLabelModel>();
            PrepareContentLabelModel(model);
            return View(model);

        }


        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(ContentLabelModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var entity = _labelService.GetContentLabelById(model.Id);
                entity = model.MapTo<ContentLabelModel, ContentLabel>(entity);
                _labelService.UpdateContentLabel(entity);

                if (continueEditing)
                {
                    return RedirectToAction("Edit", new { id = model.Id });
                }
                return RedirectToAction("Index");
            }

            PrepareContentLabelModel(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _labelService.DeleteContentLabel(id);
            return new NullJsonResult();
        }
        #endregion
    }
}