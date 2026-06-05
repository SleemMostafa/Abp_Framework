using Wesaya.Localization;
using Volo.Abp.Application.Services;

namespace Wesaya;

public abstract class WesayaAppService : ApplicationService
{
    protected WesayaAppService()
    {
        LocalizationResource = typeof(WesayaResource);
    }
}
