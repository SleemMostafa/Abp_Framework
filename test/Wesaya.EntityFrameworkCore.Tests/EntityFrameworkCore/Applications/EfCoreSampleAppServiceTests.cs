using Wesaya.Samples;
using Xunit;

namespace Wesaya.EntityFrameworkCore.Applications;

[Collection(WesayaTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<WesayaEntityFrameworkCoreTestModule>
{

}
