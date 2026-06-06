using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Domain.Values;
using Wesaya.Localization;

namespace Wesaya.Menu;

public class ExtraItem : ValueObject
{
    public StrongLocalizedString Name { get; private set; } = null!;

    public decimal Price { get; private set; }

    private ExtraItem()
    {
    }

    private ExtraItem(StrongLocalizedString name, decimal price)
    {
        Name = name;
        SetPrice(price);
    }

    public static ExtraItem Create(StrongLocalizedString name, decimal price)
    {
        return new ExtraItem(name, price);
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
