using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCommunitySolution.CacheNames
{

    public partial class ModelCacheEventConsumer
    {
        /// <summary>
        /// Key for categories caching
        /// </summary>
        /// <remarks>
        /// {0} : show hidden records?
        /// </remarks>
        public const string CATEGORIES_LIST_KEY = "net.pres.admin.categories.list-{0}";
        public const string CATEGORIES_LIST_PATTERN_KEY = "net.pres.admin.categories.list";
    }
}
