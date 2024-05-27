using Volo.Abp.Settings;

namespace Eshop.Settings;

public class EshopSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(EshopSettings.MySetting1));
    }
}
