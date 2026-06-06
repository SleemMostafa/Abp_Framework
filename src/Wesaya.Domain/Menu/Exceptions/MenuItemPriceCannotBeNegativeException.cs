using Volo.Abp;

namespace Wesaya.Menu.Exceptions;

public class MenuItemPriceCannotBeNegativeException()
    : BusinessException(WesayaErrorCodes.MenuItemPriceCannotBeNegative);
