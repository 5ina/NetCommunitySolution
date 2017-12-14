using Abp.Application.Services;
using Abp.Application.Services.Dto;
using NetCommunitySolution.Domain.Catalog;
using System.Collections.Generic;

namespace NetCommunitySolution.Catalog
{
    /// <summary>
    /// 标签服务接口
    /// </summary>
    public interface IContentLabelService: IApplicationService
    {
        /// <summary>
        /// 新增标签
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        int InsertContentLabel(ContentLabel label);

        /// <summary>
        /// 更新标签
        /// </summary>
        /// <param name="label"></param>
        void UpdateContentLabel(ContentLabel label);

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="labelId"></param>
        void DeleteContentLabel(int labelId);

        /// <summary>
        /// 根据主键获取标签
        /// </summary>
        /// <param name="labelId"></param>
        /// <returns></returns>
        ContentLabel GetContentLabelById(int labelId);

        /// <summary>
        /// 获取所有的标签
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="categoryId"></param>
        /// <param name="categoryIds"></param>
        /// <param name="showHidden"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IPagedResult<ContentLabel> GetAllLabels(string keywords = "",
            int? categoryId = null, int[] categoryIds = null, 
            bool showHidden = false,
            int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// 根据类别Id获取标签
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="showHidden"></param>
        /// <returns></returns>
        IList<ContentLabel> GetLabelsByCategoryId(int categoryId, bool showHidden = false);


        /// <summary>
        /// 根据类别Ids获取标签
        /// </summary>
        /// <param name="categoryIds"></param>
        /// <param name="showHidden"></param>
        /// <returns></returns>
        IList<ContentLabel> GetLabelsByCategoryId(int[] categoryIds, bool showHidden = false);
    }
}
