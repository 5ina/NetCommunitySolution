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
    public class PostLabelService: NetCommunitySolutionAppServiceBase, IPostLabelService
    {
        #region Fields
        private readonly IRepository<PostLabel> _postLabelRepository;
        private readonly ICacheManager _cacheManager;


        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : label ID
        /// </remarks>
        private const string POSTLABELS_BY_ID_KEY = "net.postlabel.id-{0}";


        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : post ID
        /// {1} : label ID
        /// </remarks>
        private const string POSTLABELS_BY_PARENT_ID_KEY = "net.postlabel.by-{0}-{1}";

        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string POSTLABELS_PATTERN_KEY = "net.postlabel.";

        #endregion

        #region Ctor


        public PostLabelService(IRepository<PostLabel> postLabelRepository, ICacheManager cacheManager)
        {
            this._postLabelRepository = postLabelRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Method
        public void InsertPostLabel(PostLabel label)
        {
            if (label == null)
                throw new ArgumentNullException("label");

            _postLabelRepository.Insert(label);

            //cache
            _cacheManager.RemoveByPattern(POSTLABELS_PATTERN_KEY);
            
        }

        public void InsertPostLabel(IList<PostLabel> labels)
        {
            foreach (var item in labels)
            {
                if (item == null)
                    throw new ArgumentNullException("label");

                _postLabelRepository.Insert(item);
            }
            _cacheManager.RemoveByPattern(POSTLABELS_PATTERN_KEY);
        }

        public void DeletePostLabel(int postLabelId)
        {
            _postLabelRepository.Delete(postLabelId);
        }

        public IList<PostLabel> GetPostLabelsByPostId(int postId)
        {
            var key = string.Format(POSTLABELS_BY_PARENT_ID_KEY, postId, 0);
            return _cacheManager.GetCache(key).Get(key, () => {
                var labels = from l in _postLabelRepository.GetAll()
                             where postId == l.PostId
                             orderby l.Id descending
                             select l;
                return labels.ToList();
            });
        }

        public IList<PostLabel> GetPostLabelsByLabelId(int labelId)
        {
            var key = string.Format(POSTLABELS_BY_PARENT_ID_KEY, 0, labelId);
            return _cacheManager.GetCache(key).Get(key, () => {
                var labels = from l in _postLabelRepository.GetAll()
                             where labelId == l.LabelId
                             orderby l.Id descending
                             select l;
                return labels.ToList();
            });
        }
        #endregion
    }
}
