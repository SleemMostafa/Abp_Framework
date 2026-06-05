using Volo.Abp.Modularity;

namespace MyCompany.MyProject;

public abstract class MyProjectApplicationTestBase<TStartupModule> : MyProjectTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
