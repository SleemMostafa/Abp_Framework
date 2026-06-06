using Volo.Abp;

namespace Wesaya.Menu.Exceptions;

public sealed class MenuItemPriceCannotBeNegativeException()
    : BusinessException(WesayaErrorCodes.MenuItemPriceCannotBeNegative);
