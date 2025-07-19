using Muyik.SmartSchool.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Muyik.SmartSchool.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(SmartSchoolEntityFrameworkCoreModule),
    typeof(SmartSchoolApplicationContractsModule)
)]
public class SmartSchoolDbMigratorModule : AbpModule
{
}
