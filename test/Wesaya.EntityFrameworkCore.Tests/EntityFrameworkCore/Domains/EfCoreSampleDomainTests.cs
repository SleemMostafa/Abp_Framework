using Wesaya.Samples;
using Xunit;

namespace Wesaya.EntityFrameworkCore.Domains;

[Collection(WesayaTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<WesayaEntityFrameworkCoreTestModule>
{

}
