using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using FluentValidation;
using Wesaya.Validation;

namespace Wesaya;

[DependsOn(
    typeof(WesayaDomainModule),
    typeof(AbpAccountApplicationModule),
    typeof(WesayaApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule)
    )]
public class WesayaApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMapperlyObjectMapper<WesayaApplicationModule>();
        context.Services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(WesayaApplicationModule).Assembly);
        });

        context.Services.AddValidatorsFromAssembly(typeof(WesayaApplicationModule).Assembly);
        context.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
}
