using Xunit;

namespace Level15.Logistics.Tests;

public sealed class BehaviorTests
{
    [Theory]
    [InlineData("car", 100, 100)]
    [InlineData("bike", 10, 40)]
    [InlineData("walk", 5, 60)]
    public void Plan_estimates_minutes_according_to_the_mode(string mode, int distanceKm, int expected)
    {
        Assert.Equal(expected, new RoutePlanner().Plan(mode, distanceKm));
    }

    [Fact]
    public void An_unknown_mode_is_rejected()
    {
        Assert.Throws<ArgumentException>(() => new RoutePlanner().Plan("teleport", 10));
    }
}
