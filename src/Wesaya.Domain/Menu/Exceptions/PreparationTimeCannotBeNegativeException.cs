using Volo.Abp;

namespace Wesaya.Menu.Exceptions;

public sealed class PreparationTimeCannotBeNegativeException()
    : BusinessException(WesayaErrorCodes.PreparationTimeCannotBeNegative);
