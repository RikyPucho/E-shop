using Volo.Abp.Modularity;

namespace Eshop;

[DependsOn(
    typeof(EshopApplicationModule),
    typeof(EshopDomainTestModule)
)]
public class EshopApplicationTestModule : AbpModule
{

}
