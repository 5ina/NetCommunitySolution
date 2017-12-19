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
using System.Linq;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Areas.Operate.Controllers
{
    public class CatalogController : AdminBaseController
    {

        #region Fields && Ctor

        private readonly ICategoryService _categoryService;
        private readonly ICacheManager _cacheManager;

        public CatalogController(ICategoryService categoryService, ICacheManager cacheManager)
        {
            this._categoryService = categoryService;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Utilities
        [NonAction]
        protected virtual void PrepareCategoryModel(CategoryModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            var allCategories = SelectListHelper.GetCategoryList(_categoryService, _cacheManager, true);
            foreach (var c in allCategories)
            {
                c.Selected = model.SelectedCategoryIds.Contains(int.Parse(c.Value));
                model.AvailableCategories.Add(c);
            }
            model.AvailableCategories.Insert(0, new SelectListItem
            {
                Text = "顶级类别",
                Value = "0",
                Selected = model.ParentCategoryId == 0
            });
        }
        #endregion

        #region Method
        public ActionResult Index()
        {
            var model = new CategoryListModel();
            return View(model);
        }
        

        [HttpPost]
        public ActionResult List(DataSourceRequest command,string Keywords)
        {
            var categories = _categoryService.GetAllCategories(keywords: Keywords,
                showHidden: true,
                pageIndex: command.pageName - 1,
                pageSize: command.limitName);

            var data = new DataSourceResult
            {
                count = categories.TotalCount,
                data = categories.Items.Select(c => new
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
            var model = new CategoryModel();
            PrepareCategoryModel(model);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(CategoryModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var entity = model.MapTo<Category>();

                var categoryId = _categoryService.InsertCategory(entity);

                if (continueEditing)
                {
                    return RedirectToAction("Edit", new { id = categoryId });
                }
                return RedirectToAction("Index");
            }

            PrepareCategoryModel(model);
            return View();
        }

        public ActionResult Edit(int id)
        {
            var entity = _categoryService.GetCategoryById(id);
            var model = entity.MapTo<CategoryModel>();
            PrepareCategoryModel(model);
            return View(model);
        }


        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(CategoryModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var entity = _categoryService.GetCategoryById(model.Id);
                entity = model.MapTo<CategoryModel, Category>(entity);

                _categoryService.UpdateCategory(entity);

                if (continueEditing)
                {
                    return RedirectToAction("Edit", new { id = model.Id });
                }
                return RedirectToAction("Index");
            }

            PrepareCategoryModel(model);
            return View();
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            _categoryService.DeleteCategory(id);
            return new NullJsonResult();
        }
        #endregion
    }
}