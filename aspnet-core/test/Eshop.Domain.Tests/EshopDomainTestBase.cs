using Volo.Abp.Modularity;

namespace Eshop;

/* Inherit from this class for your domain layer tests. */
public abstract class EshopDomainTestBase<TStartupModule> : EshopTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
