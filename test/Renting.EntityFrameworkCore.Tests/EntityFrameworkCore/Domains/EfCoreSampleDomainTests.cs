using Renting.Samples;
using Xunit;

namespace Renting.EntityFrameworkCore.Domains;

[Collection(RentingTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<RentingEntityFrameworkCoreTestModule>
{

}
