using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Muyik.SmartSchool.Data;
using Volo.Abp.DependencyInjection;

namespace Muyik.SmartSchool.EntityFrameworkCore;

public class EntityFrameworkCoreSmartSchoolDbSchemaMigrator
    : ISmartSchoolDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreSmartSchoolDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the SmartSchoolDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<SmartSchoolDbContext>()
            .Database
            .MigrateAsync();
    }
}
