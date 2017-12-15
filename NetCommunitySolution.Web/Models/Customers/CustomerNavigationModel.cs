using System.Collections.Generic;

namespace NetCommunitySolution.Web.Models.Customers
{
    public class CustomerNavigationModel
    {
        public CustomerNavigationModel()
        {
            CustomerNavigationItems = new List<CustomerNavigationItemModel>();
        }

        public IList<CustomerNavigationItemModel> CustomerNavigationItems { get; set; }

        public CustomerNavigationEnum SelectedTab { get; set; }
    }

    public class CustomerNavigationItemModel
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public CustomerNavigationEnum Tab { get; set; }
        public string ItemClass { get; set; }
    }

    /// <summary>
    /// 用户导航
    /// </summary>
    public enum CustomerNavigationEnum
    {
        Info = 0,
        Posts =10,
        RewardPoints = 60,
        ChangePassword = 70,
        Avatar = 80,
    }
}