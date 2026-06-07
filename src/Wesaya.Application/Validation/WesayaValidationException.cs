using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp;
using Volo.Abp.Validation;

namespace Wesaya.Validation;

public sealed class WesayaValidationException(IList<ValidationResult> validationErrors)
    : BusinessException(WesayaErrorCodes.ValidationError), IHasValidationErrors
{
    public IList<ValidationResult> ValidationErrors { get; } = validationErrors;
}
