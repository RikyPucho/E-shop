using Eshop.Samples;
using Xunit;

namespace Eshop.EntityFrameworkCore.Applications;

[Collection(EshopTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<EshopEntityFrameworkCoreTestModule>
{

}
