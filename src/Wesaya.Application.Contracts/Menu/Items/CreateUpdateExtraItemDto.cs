using Wesaya.Menu.Shared;

namespace Wesaya.Menu.Items;

public sealed class CreateUpdateExtraItemDto
{
    public StrongLocalizedStringInputDto Name { get; init; } = new();

    public decimal Price { get; init; }
}
