using System;
using System.Collections.Generic;
using System.Text;
using Eshop.Localization;
using Volo.Abp.Application.Services;

namespace Eshop;

/* Inherit your application services from this class.
 */
public abstract class EshopAppService : ApplicationService
{
    protected EshopAppService()
    {
        LocalizationResource = typeof(EshopResource);
    }
}
