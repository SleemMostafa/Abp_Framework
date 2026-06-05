using Volo.Abp.Modularity;

namespace Wesaya;

[DependsOn(
    typeof(WesayaApplicationModule),
    typeof(WesayaDomainTestModule)
)]
public class WesayaApplicationTestModule : AbpModule
{

}
