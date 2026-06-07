using Volo.Abp;

namespace Wesaya.Menu.Exceptions;

public sealed class MenuItemNotFoundException()
    : BusinessException(WesayaErrorCodes.MenuItemNotFound);
