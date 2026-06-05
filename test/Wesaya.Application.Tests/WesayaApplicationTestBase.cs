using Volo.Abp.Modularity;

namespace Wesaya;

public abstract class WesayaApplicationTestBase<TStartupModule> : WesayaTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
