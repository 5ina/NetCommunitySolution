using Abp.AutoMapper;
using Abp.Domain.Uow;
using Abp.Runtime.Caching;
using NetCommunitySolution.Catalog;
using NetCommunitySolution.Common;
using NetCommunitySolution.Customers;
using NetCommunitySolution.Domain.Catalog;
using NetCommunitySolution.Domain.Customers;
using NetCommunitySolution.Web.Framework.Controllers;
using NetCommunitySolution.Web.Framework.Html;
using NetCommunitySolution.Web.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Controllers
{
    public class PostController : NetCommunitySolutionControllerBase
    {


        #region Fields && Ctor

        private readonly ICategoryService _categoryService;
        private readonly IPostService _postService;
        private readonly IContentLabelService _labelService;
        private readonly ISettingService _settingService;
        private readonly ICustomerService _customerService;
        private readonly IPostCommentService _commentService;
        private readonly IPostLabelService _postLabelService;
        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : category ID
        /// {1} : show hidden records?
        /// </remarks>
        private const string LABEL_BY_CATEGORY_ID_KEY = "net.label.bycategory-{0}-{1}";
        public PostController(ICategoryService categoryService,
            IPostService postService,
            IContentLabelService labelService,
            ISettingService settingService,
            ICustomerService customerService,
            IPostLabelService postLabelService,
            IPostCommentService commentService,
        ICacheManager cacheManager)
        {
            this._categoryService = categoryService;
            this._postService = postService;
            this._labelService = labelService;
            this._settingService = settingService;
            this._customerService = customerService;
            this._cacheManager = cacheManager;
            this._postLabelService = postLabelService;
            this._commentService = commentService;
        }

        #endregion

        #region Utilities

        [NonAction]
        protected virtual void PreparePostModel(PostModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");


            var allCategories = SelectListHelper.GetCategoryList(_categoryService, _cacheManager, true);
            foreach (var c in allCategories)
            {
                model.AvailableCategories.Add(c);
            }
            model.AvailableCategories.Insert(0, new SelectListItem
            {
                Text = "选择分类",
                Value = "0"
            });

        }


        [NonAction]
        protected virtual void PreparePostDetailModel(PostDetailModel model,Post entity)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            var labels = _postLabelService.GetPostLabelsByPostId(entity.Id);
            model.ContentLabels = labels.Select(l =>
            {
                var label = _labelService.GetContentLabelById(l.LabelId);
                var labelModel = label.MapTo<ContentLabelModel>();
                return labelModel;
            }).ToList();
        }
        #endregion
        #region Method

        public ActionResult New()
        {
            var model = new PostModel();
            PreparePostModel(model);
            return View(model);
        }

        [UnitOfWork]
        [ValidateInput(false)]
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult NewPost(PostModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var entity = model.MapTo<Post>();
                model.Id = _postService.InsertPost(entity);
                var postLabels = model.ContentLabelIds.Select(l => new PostLabel
                {
                    LabelId = l,
                    PostId = model.Id,
                }).ToList();
                _postLabelService.InsertPostLabel(postLabels);

                if (continueEditing)
                {
                    return RedirectToAction("Edit", new { id = model.Id });
                }
                return RedirectToAction("Detail", new { id = model.Id });
            }

            PreparePostModel(model);
            return View(model);
        }
        #endregion

        #region  Child Post
        [HttpPost]
        public ActionResult GetCategoryLabel(int categoryId)
        {
            var key = string.Format(LABEL_BY_CATEGORY_ID_KEY, categoryId, false);

            var data = _cacheManager.GetCache(key).Get(key, () =>
            {
                var labels = _labelService.GetAllLabels(categoryId: categoryId);

                var items = labels.Items.Select(l => new
                {
                    Title = l.Name,
                    Value = l.Id
                });
                return items;
            });

            return AbpJson(data);
        }


        [ChildActionOnly]
        public ActionResult HotPost()
        {
            var setting = _settingService.GetPostSettings();
            var posts =_postService.GetAllPost(pageSize: setting.HotPostsCount);
            var items = posts.Items.Select(p => {
                var model = p.MapTo<SimplePostModel>();
                var customer = _customerService.GetCustomerId(Convert.ToInt32(p.CreatorUserId));
                model.CustomerName = customer.NickName;
                return model;
            }).ToList();
            return PartialView(items);
        }

        public ActionResult Detail(int postId)
        {
            var entity = _postService.GetPostById(postId);
            entity.Views += 1;
            _postService.UpdatePost(entity);
            var model = entity.MapTo<PostDetailModel>();
            PreparePostDetailModel(model, entity);
            return View(model);
        }
        #endregion

        #region Comment
        [ChildActionOnly]
        public ActionResult PostComment(int postId)
        {
            var comments = _commentService.GetCommentByPostId(postId);

            var items = comments.Select(c =>
            {
                var model = new CustomerPostCommentModel();
                model.Comment = c.MapTo<PostCommentModel>();
                model.CustomerId = Convert.ToInt32(c.CreatorUserId);
                var customer = _customerService.GetCustomerId(model.CustomerId);
                model.NickName = customer.NickName;
                model.Avatar = customer.GetCustomerAttributeValue<string>(CustomerAttributeNames.Avatar);
                return model;
            }).ToList();
            return PartialView(items);
        }

        [ChildActionOnly]
        public ActionResult Comment(int postId)
        {
            var model = new PostCommentModel();
            model.PostId = postId;
            model.CreatorUserId = AbpSession.UserId;
            return PartialView(model);
        }
        
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Comment(PostCommentModel model)
        {
            if (model.PostId <= 0)
                return AbpJson(null);

            var entity = model.MapTo<PostComment>();
            _commentService.InsertComment(entity);
            return AbpJson("success");            
        }

        [UnitOfWork]
        [HttpPost]
        public ActionResult SelectComment(int commentId, int postId)
        {
            var post = _postService.GetPostById(postId);
            post.Solve = true;
            _postService.UpdatePost(post);
            var comment = _commentService.GetPostComment(commentId);
            comment.Selected = true;
            _commentService.UpdateComment(comment);
            return AbpJson("success");
        }
        #endregion


        #region PostList
        public ActionResult LabelPost(int labelId)
        {
            var contentLabel = _labelService.GetContentLabelById(labelId);
            var model = contentLabel.MapTo<ContentLabelModel>();
            return View(model);
        }
        #endregion
    }
}