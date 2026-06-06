using System.Collections.Generic;
using System.Globalization;
using Volo.Abp;
using Volo.Abp.Domain.Values;

namespace Wesaya.Localization;

public sealed class OptionalLocalizedString : ValueObject
{
    public string? English { get; private set; }

    public string? Arabic { get; private set; }

    private OptionalLocalizedString()
    {
    }

    private OptionalLocalizedString(string? english, string? arabic, int maxLength)
    {
        English = Check.Length(english, nameof(english), maxLength);
        Arabic = Check.Length(arabic, nameof(arabic), maxLength);
    }

    public static OptionalLocalizedString Create(string? english, string? arabic, int maxLength)
    {
        return new OptionalLocalizedString(english, arabic, maxLength);
    }

    public static OptionalLocalizedString Empty()
    {
        return new OptionalLocalizedString();
    }

    public string? GetValue(CultureInfo? culture = null)
    {
        culture ??= CultureInfo.CurrentUICulture;

        return culture.TwoLetterISOLanguageName == "ar"
            ? Arabic
            : English;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return English ?? string.Empty;
        yield return Arabic ?? string.Empty;
    }
}
