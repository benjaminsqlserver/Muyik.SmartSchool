using Muyik.SmartSchool.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Muyik.SmartSchool.Blazor.Client;

public abstract class SmartSchoolComponentBase : AbpComponentBase
{
    protected SmartSchoolComponentBase()
    {
        LocalizationResource = typeof(SmartSchoolResource);
    }
}
