using Volo.Abp.Modularity;

namespace Muyik.SmartSchool;

[DependsOn(
    typeof(SmartSchoolApplicationModule),
    typeof(SmartSchoolDomainTestModule)
)]
public class SmartSchoolApplicationTestModule : AbpModule
{

}
