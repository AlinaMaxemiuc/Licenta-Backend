using Renting.Samples;
using Xunit;

namespace Renting.EntityFrameworkCore.Applications;

[Collection(RentingTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<RentingEntityFrameworkCoreTestModule>
{

}
