using Microsoft.Extensions.Localization;
using Muyik.SmartSchool.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Muyik.SmartSchool;

[Dependency(ReplaceServices = true)]
public class SmartSchoolBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<SmartSchoolResource> _localizer;

    public SmartSchoolBrandingProvider(IStringLocalizer<SmartSchoolResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
