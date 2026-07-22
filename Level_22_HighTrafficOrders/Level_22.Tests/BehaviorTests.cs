using Xunit;

namespace Level22.Orders.Tests;

public sealed class BehaviorTests
{
    [Fact]
    public void An_order_can_be_placed_then_read_back()
    {
        var api = new OrderApi();

        api.Place("A1", 1500m);
        var summary = api.GetSummary("A1");

        Assert.Equal("A1", summary.OrderId);
        Assert.Equal(1500m, summary.Amount);
        Assert.Equal("Priority", summary.Status);
    }

    [Fact]
    public void Small_orders_are_standard()
    {
        var api = new OrderApi();

        api.Place("B2", 200m);

        Assert.Equal("Standard", api.GetSummary("B2").Status);
    }
}
