using Xunit;

namespace Level08.Payments.Tests;

public sealed class BehaviorTests
{
    [Fact]
    public void A_valid_payment_succeeds_and_returns_a_reference()
    {
        var receipt = PaymentGateway.Create().Pay(100m, "USD");

        Assert.True(receipt.Success);
        Assert.Equal("TXN-USD-10000", receipt.Reference);
    }

    [Fact]
    public void A_non_positive_amount_is_declined()
    {
        var receipt = PaymentGateway.Create().Pay(0m, "USD");

        Assert.False(receipt.Success);
    }
}
