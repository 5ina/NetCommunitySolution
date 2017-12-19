namespace NetCommunitySolution.Web.Framework.Event.Reward
{
    /// <summary>
    /// 积分事件枚举
    /// </summary>
    public enum RewardEventMode : int
    {
        /// <summary>
        /// 新帖
        /// </summary>
        NewPost = 30,
        /// <summary>
        /// 评论
        /// </summary>
        Comment = 40,
        /// <summary>
        /// 选中
        /// </summary>
        Selected = 50,
        /// <summary>
        /// 结贴
        /// </summary>
        Solve = 60

    }
}