using Volo.Abp.Modularity;

namespace Wesaya;

/* Inherit from this class for your domain layer tests. */
public abstract class WesayaDomainTestBase<TStartupModule> : WesayaTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
