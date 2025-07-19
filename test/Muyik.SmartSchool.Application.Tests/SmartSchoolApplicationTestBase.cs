using Volo.Abp.Modularity;

namespace Muyik.SmartSchool;

public abstract class SmartSchoolApplicationTestBase<TStartupModule> : SmartSchoolTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
