using Muyik.SmartSchool.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Muyik.SmartSchool.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class SmartSchoolController : AbpControllerBase
{
    protected SmartSchoolController()
    {
        LocalizationResource = typeof(SmartSchoolResource);
    }
}
