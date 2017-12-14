using System.ComponentModel;
using System.Web.Mvc;

namespace NetCommunitySolution.Web.Areas.Operate.Models.Catalog
{
    public class CategoryListModel
    {

        [DisplayName("关键词")]
        [AllowHtml]
        public string Keywords { get; set; }
        
    }
}