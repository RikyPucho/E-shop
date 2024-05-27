using Xunit;

namespace Eshop.EntityFrameworkCore;

[CollectionDefinition(EshopTestConsts.CollectionDefinitionName)]
public class EshopEntityFrameworkCoreCollection : ICollectionFixture<EshopEntityFrameworkCoreFixture>
{

}
