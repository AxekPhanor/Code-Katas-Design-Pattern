using Xunit;

namespace Level24.Checkout.Tests;

public sealed class BehaviorTests
{
    [Fact]
    public void A_successful_checkout_runs_every_step_in_order()
    {
        var log = Checkout.Run(paymentSucceeds: true);

        Assert.Equal(
            new[] { "Stock reserved", "Payment charged", "Shipping scheduled" },
            log);
    }

    [Fact]
    public void A_failed_payment_compensates_the_completed_steps()
    {
        var log = Checkout.Run(paymentSucceeds: false);

        Assert.Equal(
            new[] { "Stock reserved", "Stock released" },
            log);
    }
}
