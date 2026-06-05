using Volo.Abp.Settings;

namespace Wesaya.Settings;

public class WesayaSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(WesayaSettings.MySetting1));
    }
}
