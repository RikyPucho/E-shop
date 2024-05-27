using Volo.Abp.Modularity;

namespace Eshop;

public abstract class EshopApplicationTestBase<TStartupModule> : EshopTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
