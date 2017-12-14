namespace NetCommunitySolution.Web.Framework.Layui
{
    /// <summary>
    /// layui的请求类
    /// </summary>
    public class DataSourceRequest
    {

        public int pageName { get; set; }

        public int limitName { get; set; }

        public DataSourceRequest()
        {
            this.pageName = 1;
            this.limitName = 20;
        }
    }
}