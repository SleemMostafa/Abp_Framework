namespace Wesaya.Menu.Dtos;

public class CreateUpdateExtraItemDto
{
    public StrongLocalizedStringInputDto Name { get; set; } = new();

    public decimal Price { get; set; }
}
