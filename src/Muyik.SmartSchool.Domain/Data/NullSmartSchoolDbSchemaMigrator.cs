using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Muyik.SmartSchool.Data;

/* This is used if database provider does't define
 * ISmartSchoolDbSchemaMigrator implementation.
 */
public class NullSmartSchoolDbSchemaMigrator : ISmartSchoolDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
