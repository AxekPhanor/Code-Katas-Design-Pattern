using Xunit;

namespace Level19.Taxation.Tests;

public sealed class BehaviorTests
{
    [Fact]
    public void Total_tax_sums_the_tax_of_every_item_type()
    {
        var items = new ITaxItem[]
        {
            new Salary(1000m),
            new Dividend(500m),
        };

        // 1000 * 30% + 500 * 15% = 300 + 75 = 375
        Assert.Equal(375m, TaxReport.TotalTax(items));
    }

    [Fact]
    public void An_empty_portfolio_owes_nothing()
    {
        Assert.Equal(0m, TaxReport.TotalTax(Array.Empty<ITaxItem>()));
    }
}
