using Volo.Abp;

namespace Wesaya.Menu.Exceptions;

public class MenuCategoryNotFoundException(string categoryName)
    : BusinessException(WesayaErrorCodes.MenuCategoryNotFound)
{
    public string CategoryName { get; } = categoryName;
}
