using Volo.Abp;

namespace Wesaya.Menu.Exceptions;

public class ExtraItemNotFoundException()
    : BusinessException(WesayaErrorCodes.ExtraItemNotFound);
