using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Domain.Values;

namespace Wesaya.Menu;

public class ExtraItem : ValueObject
{
    public string Name { get; private set; } = string.Empty;

    public decimal Price { get; private set; }

    private ExtraItem()
    {
        Name = string.Empty;
    }

    private ExtraItem(string name, decimal price)
    {
        SetName(name);
        SetPrice(price);
    }

    public static ExtraItem Create(string name, decimal price)
    {
        return new ExtraItem(name, price);
    }

    private void SetName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(
            name,
            nameof(name),
            MenuConsts.MaxExtraItemNameLength);
    }

    private void SetPrice(decimal price)
    {
        if (price < 0)
        {
            throw new BusinessException("Wesaya:ExtraItemPriceCannotBeNegative");
        }

        Price = price;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Name;
        yield return Price;
    }
}
