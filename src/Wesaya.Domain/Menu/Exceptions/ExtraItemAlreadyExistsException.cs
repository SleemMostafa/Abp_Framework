using Volo.Abp;

namespace Wesaya.Menu.Exceptions;

public sealed class ExtraItemAlreadyExistsException()
    : BusinessException(WesayaErrorCodes.ExtraItemAlreadyExists);
