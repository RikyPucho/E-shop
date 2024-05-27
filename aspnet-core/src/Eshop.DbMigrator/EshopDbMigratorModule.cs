using Eshop.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Eshop.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(EshopEntityFrameworkCoreModule),
    typeof(EshopApplicationContractsModule)
    )]
public class EshopDbMigratorModule : AbpModule
{
}
