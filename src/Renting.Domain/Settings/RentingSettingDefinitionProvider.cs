using Volo.Abp.Settings;

namespace Renting.Settings;

public class RentingSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(RentingSettings.MySetting1));
    }
}
