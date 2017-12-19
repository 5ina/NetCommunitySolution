using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace NetCommunitySolution.Web.Models.Common
{
    public class PagedItemModel<T> where T : EntityDto
    {
        public PagedItemModel()
        {
            this.Items = new List<T>();
            PageIndex = 0;
            PageSize = int.MaxValue;
        }
        public IList<T> Items { get; set; }
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int Total { get; set; }
    }
}