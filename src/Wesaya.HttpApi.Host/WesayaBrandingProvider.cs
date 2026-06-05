using Microsoft.Extensions.Localization;
using Wesaya.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Wesaya;

[Dependency(ReplaceServices = true)]
public class WesayaBrandingProvider(IStringLocalizer<WesayaResource> localizer) : DefaultBrandingProvider
{
    public override string AppName => localizer["AppName"];
}
