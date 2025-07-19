using Volo.Abp.Modularity;

namespace Muyik.SmartSchool;

/* Inherit from this class for your domain layer tests. */
public abstract class SmartSchoolDomainTestBase<TStartupModule> : SmartSchoolTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
