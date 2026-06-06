using Volo.Abp;

namespace Wesaya.Menu.Exceptions;

public class ExtraItemAlreadyExistsException()
    : BusinessException(WesayaErrorCodes.ExtraItemAlreadyExists);
