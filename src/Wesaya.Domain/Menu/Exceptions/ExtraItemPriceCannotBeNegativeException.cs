using Volo.Abp;

namespace Wesaya.Menu.Exceptions;

public class ExtraItemPriceCannotBeNegativeException()
    : BusinessException(WesayaErrorCodes.ExtraItemPriceCannotBeNegative);
