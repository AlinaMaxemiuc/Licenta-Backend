using Xunit;

namespace Renting.EntityFrameworkCore;

[CollectionDefinition(RentingTestConsts.CollectionDefinitionName)]
public class RentingEntityFrameworkCoreCollection : ICollectionFixture<RentingEntityFrameworkCoreFixture>
{

}
