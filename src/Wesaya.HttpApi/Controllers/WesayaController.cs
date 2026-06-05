using Wesaya.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Wesaya.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class WesayaController : AbpControllerBase
{
    protected WesayaController()
    {
        LocalizationResource = typeof(WesayaResource);
    }
}
