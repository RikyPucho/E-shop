using Volo.Abp.Modularity;

namespace Eshop;

[DependsOn(
    typeof(EshopDomainModule),
    typeof(EshopTestBaseModule)
)]
public class EshopDomainTestModule : AbpModule
{

}
