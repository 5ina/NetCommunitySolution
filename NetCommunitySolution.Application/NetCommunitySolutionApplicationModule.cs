using System.Reflection;
using Abp.Modules;

namespace NetCommunitySolution
{
    [DependsOn(typeof(NetCommunitySolutionCoreModule))]
    public class NetCommunitySolutionApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
