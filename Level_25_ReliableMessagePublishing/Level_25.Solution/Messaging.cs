namespace Level25.Messaging;

/// <summary>Un bus de messages, qui peut être momentanément indisponible.</summary>
public sealed class InMemoryBus
{
    private readonly List<string> _published = new();

    public bool IsUp { get; set; } = true;

    public IReadOnlyList<string> Published => _published;

    public void Publish(string message)
    {
        if (!IsUp)
        {
            throw new InvalidOperationException("Bus is unavailable.");
        }

        _published.Add(message);
    }
}

/// <summary>Un message en attente de publication, persisté dans l'outbox.</summary>
public sealed class OutboxMessage
{
    public OutboxMessage(string payload) => Payload = payload;

    public string Payload { get; }

    public bool Published { get; set; }
}

/// <summary>
/// L'opération écrit l'état ET un message dans l'outbox (même "transaction").
/// Un relais publie ensuite depuis l'outbox : tant qu'un message n'est pas
/// publié, il reste et sera rejoué — l'événement ne se perd jamais.
/// </summary>
public sealed class OrderProcessor
{
    private readonly InMemoryBus _bus;
    private readonly HashSet<string> _orders = new();
    private readonly List<OutboxMessage> _outbox = new();

    public OrderProcessor(InMemoryBus bus) => _bus = bus;

    public void PlaceOrder(string orderId)
    {
        _orders.Add(orderId);
        _outbox.Add(new OutboxMessage($"OrderPlaced:{orderId}"));
    }

    public void RelayOutbox()
    {
        foreach (var message in _outbox.Where(message => !message.Published))
        {
            _bus.Publish(message.Payload);
            message.Published = true;
        }
    }
}
