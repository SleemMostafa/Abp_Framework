using Volo.Abp.Modularity;

namespace Wesaya;

[DependsOn(
    typeof(WesayaDomainModule),
    typeof(WesayaTestBaseModule)
)]
public class WesayaDomainTestModule : AbpModule
{

}
