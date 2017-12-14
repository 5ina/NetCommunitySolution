using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using NetCommunitySolution.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCommunitySolution.Catalog
{
    public class ContentLabelService : NetCommunitySolutionAppServiceBase,IContentLabelService
    {

        #region Fields
        private readonly IRepository<ContentLabel> _labelRepository;
        private readonly ICacheManager _cacheManager;


        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : label ID
        /// </remarks>
        private const string LABEL_BY_ID_KEY = "net.label.id-{0}";


        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : category ID
        /// {1} : show hidden records?
        /// </remarks>
        private const string LABEL_BY_CATEGORY_ID_KEY = "net.label.bycategory-{0}-{1}";

        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string LABELS_PATTERN_KEY = "net.label.";

        #endregion

        #region Ctor


        public ContentLabelService(IRepository<ContentLabel> labelRepository, ICacheManager cacheManager)
        {
            this._labelRepository = labelRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Method

        public int InsertContentLabel(ContentLabel label)
        {
            if (label == null)
                throw new ArgumentNullException("label");

            var labelId = _labelRepository.InsertAndGetId(label);

            //cache
            _cacheManager.RemoveByPattern(LABELS_PATTERN_KEY);

            return labelId;
        }

        public void UpdateContentLabel(ContentLabel label)
        {
            if (label == null)
                throw new ArgumentNullException("label");
            

           _labelRepository.Update(label);

            //cache
            _cacheManager.RemoveByPattern(LABELS_PATTERN_KEY);
        }

        public void DeleteContentLabel(int labelId)
        {
            _labelRepository.Delete(labelId);
            //cache
            _cacheManager.RemoveByPattern(LABELS_PATTERN_KEY);
        }

        public ContentLabel GetContentLabelById(int labelId)
        {
            string key = string.Format(LABEL_BY_ID_KEY, labelId);

            return _cacheManager.GetCache(key).Get(key, () => _labelRepository.Get(labelId));
        }

        public IPagedResult<ContentLabel> GetAllLabels(string keywords = "", 
            int? categoryId = null, int[] categoryIds = null,
            bool showHidden = false,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _labelRepository.GetAll();

            if (!String.IsNullOrWhiteSpace(keywords))
                query = query.Where(l => l.Name.Contains(keywords));

            if (categoryId.HasValue)
                query = query.Where(l => l.CategoryId == categoryId.Value);

            if (categoryIds != null && categoryIds.Count() > 0)
                query = query.Where(l => categoryIds.Contains(l.CategoryId));

            if (!showHidden)
                query = query.Where(c => c.Published);

            query = query.OrderBy(c => c.CategoryId).ThenBy(c => c.DisplayOrder);

            return new PagedResult<ContentLabel>(query, pageIndex, pageSize);
        }

        public IList<ContentLabel> GetLabelsByCategoryId(int categoryId, bool showHidden = false)
        {
            var key = string.Format(LABEL_BY_CATEGORY_ID_KEY, categoryId, showHidden);
            return _cacheManager.GetCache(key).Get(key, () => {
                var query = _labelRepository.GetAll();

                var labels = from l in query
                where (!showHidden && l.Published) &&
                        l.CategoryId == categoryId
                select l;
                return labels.ToList();
            });

        }

        public IList<ContentLabel> GetLabelsByCategoryId(int[] categoryIds, bool showHidden = false)
        {
            var query = _labelRepository.GetAll();

            var labels = from l in query
                         where (!showHidden && l.Published) &&
                                categoryIds.Contains(l.CategoryId)
                         select l;
            return labels.ToList();
        }
        #endregion
    }
}
