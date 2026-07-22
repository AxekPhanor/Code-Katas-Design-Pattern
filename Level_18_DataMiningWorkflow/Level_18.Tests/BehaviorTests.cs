using Xunit;

namespace Level18.DataMining.Tests;

public sealed class BehaviorTests
{
    [Theory]
    [InlineData("csv", "Analyzed CSV[a|b|c]")]
    [InlineData("pdf", "Analyzed PDF[hello]")]
    public void Run_mines_each_format_through_the_same_workflow(string format, string expected)
    {
        Assert.Equal(expected, Workflow.Run(format));
    }

    [Fact]
    public void An_unknown_format_is_rejected()
    {
        Assert.Throws<ArgumentException>(() => Workflow.Run("xml"));
    }
}
