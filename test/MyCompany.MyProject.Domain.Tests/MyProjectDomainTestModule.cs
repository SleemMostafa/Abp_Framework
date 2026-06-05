using Volo.Abp.Modularity;

namespace MyCompany.MyProject;

[DependsOn(
    typeof(MyProjectDomainModule),
    typeof(MyProjectTestBaseModule)
)]
public class MyProjectDomainTestModule : AbpModule
{

}
