using Abp.Runtime.Caching;
using NetCommunitySolution.Catalog;
using NetCommunitySolution.Web.Framework.Layui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Areas.Operate.Controllers
{
    public class PostController : AdminBaseController
    {

        #region Fields && Ctor

        private readonly ICategoryService _categoryService;
        private readonly IPostService _postService;
        private readonly ICacheManager _cacheManager;

        public PostController(ICategoryService categoryService, 
            IPostService postService,
            ICacheManager cacheManager)
        {
            this._categoryService = categoryService;
            this._postService = postService;
            this._cacheManager = cacheManager;
        }

        #endregion
        #region Method
        // GET: Operate/Post
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult List(DataSourceRequest command, string Keywords)
        {
            var categories = _postService.GetAllPost(keywords: Keywords,
                pageIndex: command.pageName - 1,
                pageSize: command.limitName);

            var data = new DataSourceResult
            {
                count = categories.TotalCount,
                data = categories.Items.Select(c => new
                {
                    Id = c.Id,
                    Title = c.Title,
                    Views = c.Views,
                    Answer = c.Answer,
                    Solve = c.Solve ? "解决" : "未解决",
                    Support = c.Support,
                }).ToList(),
            };
            return AbpJson(data);
        }
        #endregion
    }
}