using Volo.Abp;

namespace Wesaya.Menu.Exceptions;

public class PreparationTimeCannotBeNegativeException()
    : BusinessException(WesayaErrorCodes.PreparationTimeCannotBeNegative);
