using Volo.Abp;

namespace Wesaya.Menu.Exceptions;

public sealed class MenuCategoryNotFoundException(string categoryName)
    : BusinessException(WesayaErrorCodes.MenuCategoryNotFound)
{
    public string CategoryName { get; } = categoryName;
}
