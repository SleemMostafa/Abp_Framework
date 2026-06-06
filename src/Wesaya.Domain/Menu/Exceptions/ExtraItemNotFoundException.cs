using Volo.Abp;

namespace Wesaya.Menu.Exceptions;

public sealed class ExtraItemNotFoundException()
    : BusinessException(WesayaErrorCodes.ExtraItemNotFound);
