namespace NetCommunitySolution.Web.Models.Catalog
{
    public class CustomerPostCommentModel
    {
        public CustomerPostCommentModel()
        {
            this.Comment = new PostCommentModel();
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }

        public string Avatar { get; set; }

        public PostCommentModel Comment { get; set; }
    }
}