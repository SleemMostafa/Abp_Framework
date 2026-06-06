using Wesaya.Localization;
using Wesaya.Menu.Dtos;

namespace Wesaya.Menu;

internal static class LocalizedStringFactory
{
    public static StrongLocalizedString CreateStrong(
        StrongLocalizedStringInputDto input,
        int maxLength)
    {
        return StrongLocalizedString.Create(
            input.English,
            input.Arabic,
            maxLength);
    }

    public static OptionalLocalizedString? CreateOptional(
        OptionalLocalizedStringInputDto? input,
        int maxLength)
    {
        if (input == null)
        {
            return null;
        }

        return OptionalLocalizedString.Create(
            input.English,
            input.Arabic,
            maxLength);
    }
}
