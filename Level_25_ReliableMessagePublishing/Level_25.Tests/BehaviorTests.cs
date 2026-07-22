using Xunit;

namespace Level25.Messaging.Tests;

public sealed class BehaviorTests
{
    [Fact]
    public void An_event_survives_a_publish_outage_and_is_relayed_later()
    {
        var bus = new InMemoryBus { IsUp = false };
        var processor = new OrderProcessor(bus);

        processor.PlaceOrder("A1");

        // Rien n'a encore pu être publié (le bus était indisponible).
        Assert.Empty(bus.Published);

        // Le bus revient : le relais doit republier l'événement (jamais perdu).
        bus.IsUp = true;
        processor.RelayOutbox();

        Assert.Contains("OrderPlaced:A1", bus.Published);
    }

    [Fact]
    public void An_event_is_published_when_the_bus_is_available()
    {
        var bus = new InMemoryBus { IsUp = true };
        var processor = new OrderProcessor(bus);

        processor.PlaceOrder("B2");
        processor.RelayOutbox();

        Assert.Contains("OrderPlaced:B2", bus.Published);
    }
}
