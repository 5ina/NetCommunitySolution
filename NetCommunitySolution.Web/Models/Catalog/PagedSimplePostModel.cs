using System.Collections.Generic;

namespace NetCommunitySolution.Web.Models.Catalog
{
    public class PagedSimplePostModel
    {
        public PagedSimplePostModel()
        {
            this.Posts = new List<SimplePostModel>();
            PageIndex = 0;
            PageSize = int.MaxValue;
        }
        public IList<SimplePostModel> Posts { get; set; }
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int Total { get; set; }        
    }
}