using Microsoft.Extensions.Localization;
using MyCompany.MyProject.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace MyCompany.MyProject;

[Dependency(ReplaceServices = true)]
public class MyProjectBrandingProvider(IStringLocalizer<MyProjectResource> localizer) : DefaultBrandingProvider
{
    public override string AppName => localizer["AppName"];
}
