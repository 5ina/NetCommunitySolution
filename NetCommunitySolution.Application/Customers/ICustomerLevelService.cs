using Abp.Application.Services;
using Abp.Application.Services.Dto;
using NetCommunitySolution.Domain.Catalog;

namespace NetCommunitySolution.Customers
{
    /// <summary>
    /// 用户级别服务接口
    /// </summary>
    public interface ICustomerLevelService:IApplicationService
    {
        /// <summary>
        /// 新增级别
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        int InsertLevel(CustomerLevel level);

        /// <summary>
        /// 更新级别
        /// </summary>
        /// <param name="level"></param>
        void UpdateLevel(CustomerLevel level);

        /// <summary>
        /// 删除级别
        /// </summary>
        /// <param name="levelId"></param>
        void DeleteLevel(int levelId);

        /// <summary>
        /// 获取级别
        /// </summary>
        /// <param name="levelId"></param>
        /// <returns></returns>
        CustomerLevel GetCustomerLevelById(int levelId);

        /// <summary>
        /// 获取所有级别
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IPagedResult<CustomerLevel> GetAllLevels(string keywords = "",
            int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
