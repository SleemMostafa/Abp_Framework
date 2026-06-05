using Xunit;

namespace MyCompany.MyProject.EntityFrameworkCore;

[CollectionDefinition(MyProjectTestConsts.CollectionDefinitionName)]
public class MyProjectEntityFrameworkCoreCollection : ICollectionFixture<MyProjectEntityFrameworkCoreFixture>
{

}
