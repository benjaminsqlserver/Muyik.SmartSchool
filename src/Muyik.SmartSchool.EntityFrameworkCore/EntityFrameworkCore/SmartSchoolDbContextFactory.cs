using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Muyik.SmartSchool.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class SmartSchoolDbContextFactory : IDesignTimeDbContextFactory<SmartSchoolDbContext>
{
    public SmartSchoolDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        SmartSchoolEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<SmartSchoolDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));
        
        return new SmartSchoolDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Muyik.SmartSchool.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
