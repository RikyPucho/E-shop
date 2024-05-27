using Eshop.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Eshop.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class EshopController : AbpControllerBase
{
    protected EshopController()
    {
        LocalizationResource = typeof(EshopResource);
    }
}
