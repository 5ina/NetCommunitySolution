using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using NetCommunitySolution.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCommunitySolution.Catalog
{
    public class PostService: NetCommunitySolutionAppServiceBase ,IPostService
    {

        #region Fields
        private readonly IRepository<Post> _postRepository;
        private readonly ICacheManager _cacheManager;


        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : post ID
        /// </remarks>
        private const string POSTS_BY_ID_KEY = "net.post.id-{0}";


        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : category ID
        /// {1} : show hidden records?
        /// </remarks>
        private const string POSTS_BY_CATEGORY_ID_KEY = "net.post.bycate-{0}-{1}";

        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string POSTS_PATTERN_KEY = "net.post.";

        #endregion

        #region Ctor


        public PostService(IRepository<Post> postRepository, ICacheManager cacheManager)
        {
            this._postRepository = postRepository;
            this._cacheManager = cacheManager;
        }

        #endregion
        #region Method
        public int InsertPost(Post post)
        {
            if (post == null)
                throw new ArgumentNullException("post");

            var categoryId = _postRepository.InsertAndGetId(post);

            //cache
            _cacheManager.RemoveByPattern(POSTS_PATTERN_KEY);

            return categoryId;
        }

        public void UpdatePost(Post post)
        {
            if (post == null)
                throw new ArgumentNullException("post");
            

            _postRepository.Update(post);

            //cache
            _cacheManager.RemoveByPattern(POSTS_PATTERN_KEY);
        }

        public void DeletePost(int postId)
        {
            _postRepository.Delete(postId);
        }

        public Post GetPostById(int postId)
        {
            if (postId == 0)
                return null;

            string key = string.Format(POSTS_BY_ID_KEY, postId);
            return _cacheManager.GetCache(key).Get(key, () => _postRepository.Get(postId));
        }

        public IPagedResult<Post> GetAllPost(string keywords = "",
            int customerId = 0,
            int[] categoryIds = null,
            int[] contentLabelIds = null,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _postRepository.GetAll();

            if (!String.IsNullOrEmpty(keywords))
                query = query.Where(p => p.Title.Contains(keywords));

            if (categoryIds != null && categoryIds.Count() > 0)
                query = query.Where(p => categoryIds.Contains(p.CategoryId));

            if (customerId > 0)
                query = query.Where(p => p.CreatorUserId == customerId);

            //if (contentLabelIds != null && contentLabelIds.Count() > 0)
            //    query = query.Where(p => contentLabelIds.Contains(p.CategoryId));

            query = query.OrderByDescending(p => p.LastModificationTime);

            return new PagedResult<Post>(query, pageIndex, pageSize);


            throw new NotImplementedException();
        }
        #endregion
    }
}
