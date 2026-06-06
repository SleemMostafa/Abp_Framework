using System.Collections.Generic;
using System.Globalization;
using Volo.Abp;
using Volo.Abp.Domain.Values;

namespace Wesaya.Localization;

public class StrongLocalizedString : ValueObject
{
    public string English { get; private set; } 
    public string Arabic { get; private set; }

    private StrongLocalizedString()
    {
        English = string.Empty;
        Arabic = string.Empty;
    }

    private StrongLocalizedString(string english, string arabic, int maxLength)
    {
        English = Check.NotNullOrWhiteSpace(english, nameof(english), maxLength);
        Arabic = Check.NotNullOrWhiteSpace(arabic, nameof(arabic), maxLength);
    }

    public static StrongLocalizedString Create(string english, string arabic, int maxLength)
    {
        return new StrongLocalizedString(english, arabic, maxLength);
    }

    public string GetValue(CultureInfo? culture = null)
    {
        culture ??= CultureInfo.CurrentUICulture;

        return culture.TwoLetterISOLanguageName == "ar"
            ? Arabic
            : English;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return English;
        yield return Arabic;
    }
}
