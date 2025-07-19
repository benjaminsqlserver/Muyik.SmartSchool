using System.Threading.Tasks;

namespace Muyik.SmartSchool.Data;

public interface ISmartSchoolDbSchemaMigrator
{
    Task MigrateAsync();
}
