using Xunit;

namespace Wesaya.EntityFrameworkCore;

[CollectionDefinition(WesayaTestConsts.CollectionDefinitionName)]
public class WesayaEntityFrameworkCoreCollection : ICollectionFixture<WesayaEntityFrameworkCoreFixture>
{

}
