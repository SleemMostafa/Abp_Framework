using System;
using System.Collections.Generic;
using System.Text;
using Wesaya.Localization;
using Volo.Abp.Application.Services;

namespace Wesaya;

/* Inherit your application services from this class.
 */
public abstract class WesayaAppService : ApplicationService
{
    protected WesayaAppService()
    {
        LocalizationResource = typeof(WesayaResource);
    }
}
