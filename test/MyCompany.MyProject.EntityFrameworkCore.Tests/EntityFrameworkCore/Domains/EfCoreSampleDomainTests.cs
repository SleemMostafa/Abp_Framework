using MyCompany.MyProject.Samples;
using Xunit;

namespace MyCompany.MyProject.EntityFrameworkCore.Domains;

[Collection(MyProjectTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<MyProjectEntityFrameworkCoreTestModule>
{

}
