using Xunit;

namespace Level04.EnergyGrid.Tests;

public sealed class BehaviorTests
{
    [Theory]
    [InlineData("solar", 5)]
    [InlineData("wind", 3)]
    [InlineData("nuclear", 1000)]
    public void Create_returns_the_matching_resource(string kind, int expectedOutput)
    {
        var resource = new EnergyGrid().Create(kind);

        Assert.Equal(kind, resource.Kind);
        Assert.Equal(expectedOutput, resource.OutputMegawatts);
    }

    [Fact]
    public void Unknown_kind_is_rejected()
    {
        Assert.Throws<ArgumentException>(() => new EnergyGrid().Create("plasma"));
    }
}
