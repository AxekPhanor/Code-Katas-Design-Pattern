using Xunit;

namespace Level12.Support.Tests;

public sealed class BehaviorTests
{
    [Theory]
    [InlineData(1, "Level 1 Support")]
    [InlineData(2, "Level 2 Support")]
    [InlineData(3, "Level 3 Support")]
    public void A_ticket_is_escalated_to_the_first_tier_that_can_handle_it(int difficulty, string expected)
    {
        Assert.Equal(expected, SupportDesk.Route(new Ticket("Something broke", difficulty)));
    }

    [Fact]
    public void A_ticket_nobody_can_handle_is_rejected()
    {
        Assert.ThrowsAny<Exception>(() => SupportDesk.Route(new Ticket("Impossible", 9)));
    }
}
