using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Eshop;

[Dependency(ReplaceServices = true)]
public class EshopBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Eshop";
}
