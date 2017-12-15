using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Caching;
using NetCommunitySolution.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCommunitySolution.Catalog
{
   public  class PostCommentService: NetCommunitySolutionAppServiceBase,IPostCommentService
    {

        #region Fields
        private readonly IRepository<PostComment> _commentRepository;
        private readonly IPostService _postService;
        private readonly ICacheManager _cacheManager;


        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : comment ID
        /// </remarks>
        private const string COMMENTS_BY_ID_KEY = "net.comment.id-{0}";


        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : parent post ID
        /// </remarks>
        private const string COMMENTS_BY_POST_ID_KEY= "net.comment.bypost-{0}";

        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string COMMENTS_PATTERN_KEY = "net.comment.";

        #endregion

        #region Ctor


        public PostCommentService(IRepository<PostComment> commentRepository,
            IPostService postService,
            ICacheManager cacheManager)
        {
            this._commentRepository = commentRepository;
            this._postService = postService;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Method
        [UnitOfWork]
        public void InsertComment(PostComment comment)
        {
            if (comment == null)
                throw new ArgumentNullException("comment");

            _commentRepository.Insert(comment);
            var post = _postService.GetPostById(comment.PostId);
            post.Answer += 1;
            _postService.UpdatePost(post);

            //cache
            _cacheManager.RemoveByPattern(COMMENTS_PATTERN_KEY);
            
        }

        public void UpdateComment(PostComment comment)
        {
            if (comment == null)
                throw new ArgumentNullException("comment");


            _commentRepository.Update(comment);

            //cache
            _cacheManager.RemoveByPattern(COMMENTS_PATTERN_KEY);
        }

        public void DeleteComment(int commentId)
        {
            _commentRepository.Delete(commentId);
            _cacheManager.RemoveByPattern(COMMENTS_PATTERN_KEY);
        }

        public IList<PostComment> GetCommentByPostId(int postId)
        {
            var key = string.Format(COMMENTS_BY_POST_ID_KEY, postId);
            return _cacheManager.GetCache(key).Get(key, () => {
                var items = from c in _commentRepository.GetAll()
                where c.PostId == postId
                orderby c.CreationTime descending
                select c;
                return items.ToList();
            });
        }
        #endregion
    }
}
