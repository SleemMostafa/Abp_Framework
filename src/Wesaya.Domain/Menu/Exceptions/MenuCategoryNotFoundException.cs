using Volo.Abp;

namespace Wesaya.Menu.Exceptions;

public sealed class MenuCategoryNotFoundException()
    : BusinessException(WesayaErrorCodes.MenuCategoryNotFound);
