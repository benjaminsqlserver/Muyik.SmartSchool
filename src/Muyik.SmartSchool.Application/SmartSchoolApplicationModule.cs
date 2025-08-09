using MediatR;
using Volo.Abp.Account;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;

namespace Muyik.SmartSchool;

[DependsOn(
     
     typeof(SmartSchoolDomainModule),
        typeof(SmartSchoolApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpAccountApplicationModule),
    typeof(AbpSettingManagementApplicationModule)
    )]
public class SmartSchoolApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<SmartSchoolApplicationModule>();
        });

        // Register MediatR
        context.Services.AddMediatR(typeof(SmartSchoolApplicationModule).Assembly);

        // Register MediatR - this is the correct way
       // context.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SmartSchoolApplicationModule).Assembly));
    }
}
