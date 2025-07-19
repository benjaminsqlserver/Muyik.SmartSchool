using Volo.Abp.Settings;

namespace Muyik.SmartSchool.Settings;

public class SmartSchoolSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(SmartSchoolSettings.MySetting1));
    }
}
