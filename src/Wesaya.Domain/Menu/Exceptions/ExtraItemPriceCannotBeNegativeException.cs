using Volo.Abp;

namespace Wesaya.Menu.Exceptions;

public sealed class ExtraItemPriceCannotBeNegativeException()
    : BusinessException(WesayaErrorCodes.ExtraItemPriceCannotBeNegative);
