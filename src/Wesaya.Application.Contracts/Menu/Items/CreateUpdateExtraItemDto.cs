using Wesaya.Menu.Shared;

namespace Wesaya.Menu.Items;

public class CreateUpdateExtraItemDto
{
    public StrongLocalizedStringInputDto Name { get; set; } = new();

    public decimal Price { get; set; }
}
