using Xunit;

namespace Level05.Ui.Tests;

public sealed class BehaviorTests
{
    [Theory]
    [InlineData("windows")]
    [InlineData("mac")]
    public void Components_belong_to_the_same_platform_family(string platform)
    {
        var components = new UiToolkit().CreateComponents(platform);

        Assert.Equal(platform, components.Button.Style);
        Assert.Equal(platform, components.Checkbox.Style);
    }

    [Fact]
    public void Unknown_platform_is_rejected()
    {
        Assert.Throws<ArgumentException>(() => new UiToolkit().CreateComponents("linux"));
    }
}
