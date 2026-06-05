using MyCompany.MyProject.Samples;
using Xunit;

namespace MyCompany.MyProject.EntityFrameworkCore.Applications;

[Collection(MyProjectTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<MyProjectEntityFrameworkCoreTestModule>
{

}
