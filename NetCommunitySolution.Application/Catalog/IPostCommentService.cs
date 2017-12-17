using Abp.Application.Services;
using NetCommunitySolution.Domain.Catalog;
using System.Collections.Generic;

namespace NetCommunitySolution.Catalog
{
    /// <summary>
    /// 帖子评论服务接口
    /// </summary>
    public interface IPostCommentService: IApplicationService
    {
        /// <summary>
        /// 新增评论
        /// </summary>
        /// <param name="comment"></param>
        void InsertComment(PostComment comment);

        /// <summary>
        /// 更新评论
        /// </summary>
        /// <param name="comment"></param>
        void UpdateComment(PostComment comment);

        /// <summary>
        /// 根据主键获取评论
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        PostComment GetPostComment(int commentId);

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="commentId"></param>
        void DeleteComment(int commentId);

        /// <summary>
        /// 获取评论集合
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        IList<PostComment> GetCommentByPostId(int postId);
    }
}
