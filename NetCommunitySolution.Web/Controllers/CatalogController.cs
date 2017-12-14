using Abp.AutoMapper;
using Abp.Runtime.Caching;
using NetCommunitySolution.Catalog;
using NetCommunitySolution.Web.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Controllers
{
    public class CatalogController : NetCommunitySolutionControllerBase
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

        #endregion

        #region Method
        #endregion

        #region Child Method
        [ChildActionOnly]
        public ActionResult TopMenu()
        {
            var topCatalogs = _categoryService.GetAllCategoriesDisplayedOnHomePage();
            var models = topCatalogs.MapTo<IList<CategoryModel>>();
            return PartialView(models);
        }
        #endregion
    }
}