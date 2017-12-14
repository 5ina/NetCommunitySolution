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
    public class ContentLabelController : NetCommunitySolutionControllerBase
    {

        #region Fields && Ctor

        private readonly IContentLabelService _labelService;
        private readonly ICacheManager _cacheManager;

        public ContentLabelController(IContentLabelService labelService, ICacheManager cacheManager)
        {
            this._labelService = labelService;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Method


        #endregion

        #region Child Method
        public ActionResult HotLabel()
        {
            var labels = _labelService.GetAllLabels(pageSize: 40);
            var model = labels.Items.MapTo<IList<ContentLabelModel>>();
            return PartialView(model);
        }
        #endregion

    }
}