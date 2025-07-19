using Volo.Abp.Modularity;

namespace Muyik.SmartSchool;

[DependsOn(
    typeof(SmartSchoolDomainModule),
    typeof(SmartSchoolTestBaseModule)
)]
public class SmartSchoolDomainTestModule : AbpModule
{

}
