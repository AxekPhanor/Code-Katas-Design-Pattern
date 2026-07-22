using Xunit;

namespace Level21.Composition.Tests;

public sealed class BehaviorTests
{
    [Fact]
    public void Configuring_services_registers_every_module()
    {
        var registry = new ServiceRegistry();

        Startup.ConfigureServices(registry);

        Assert.Equal(6, registry.Count);
        Assert.True(registry.Contains("PaymentGateway"));
        Assert.True(registry.Contains("EmailSender"));
        Assert.True(registry.Contains("OrderRepository"));
    }
}
