using Abp.Application.Services;
using NetCommunitySolution.Domain.Catalog;
using System.Collections.Generic;

namespace NetCommunitySolution.Catalog
{
    /// <summary>
    /// 帖子标签服务接口
    /// </summary>
    public interface IPostLabelService: IApplicationService
    {
        /// <summary>
        /// 增加标签
        /// </summary>
        /// <param name="label"></param>
        void InsertPostLabel(PostLabel label);

        /// <summary>
        /// 增加标签
        /// </summary>
        /// <param name="labels"></param>
        void InsertPostLabel(IList<PostLabel> labels);

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="postLabelId"></param>
        void DeletePostLabel(int postLabelId);

        /// <summary>
        /// 获取帖子所关联的标签
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        IList<PostLabel> GetPostLabelsByPostId(int postId);

        /// <summary>
        /// 获取标签所关联的帖子
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        IList<PostLabel> GetPostLabelsByLabelId(int labelId);


    }
}
