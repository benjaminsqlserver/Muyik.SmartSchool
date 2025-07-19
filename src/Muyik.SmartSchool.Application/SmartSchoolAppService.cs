using Muyik.SmartSchool.Localization;
using Volo.Abp.Application.Services;

namespace Muyik.SmartSchool;

/* Inherit your application services from this class.
 */
public abstract class SmartSchoolAppService : ApplicationService
{
    protected SmartSchoolAppService()
    {
        LocalizationResource = typeof(SmartSchoolResource);
    }
}
