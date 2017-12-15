using Abp.Application.Services;
using Abp.Application.Services.Dto;
using NetCommunitySolution.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCommunitySolution.Catalog
{
    public interface IPostService: IApplicationService
    {
        /// <summary>
        /// 新增帖子
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        int InsertPost(Post post);

        /// <summary>
        /// 更新帖子
        /// </summary>
        /// <param name="post"></param>
        void UpdatePost(Post post);

        /// <summary>
        /// 删除帖子
        /// </summary>
        /// <param name="postId"></param>
        void DeletePost(int postId);

        /// <summary>
        /// 查询帖子根据ID
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        Post GetPostById(int postId);

        IPagedResult<Post> GetAllPost(string keywords = "", int[] categoryIds = null,
            
            int[] contentLabelIds = null,
            int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
