using Eshop.Samples;
using Xunit;

namespace Eshop.EntityFrameworkCore.Domains;

[Collection(EshopTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<EshopEntityFrameworkCoreTestModule>
{

}
