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

// -----------------------------------------------------------------------------
//  CODE MÉTIER À REFACTORISER
// -----------------------------------------------------------------------------
//  L'opération enregistre l'état PUIS publie l'événement directement sur le bus.
//  Si le bus est indisponible (ou si le service crashe entre les deux),
//  l'événement est PERDU : rien ne le rejoue. La publication n'est pas garantie.
//
//  Objectif : écrire l'événement dans une "boîte d'envoi" (outbox) en même temps
//  que l'état, puis laisser un relais publier depuis l'outbox — pour qu'un
//  événement survive à une panne de publication.
// -----------------------------------------------------------------------------
public sealed class OrderProcessor
{
    private readonly InMemoryBus _bus;
    private readonly HashSet<string> _orders = new();

    public OrderProcessor(InMemoryBus bus) => _bus = bus;

    public void PlaceOrder(string orderId)
    {
        _orders.Add(orderId);

        try
        {
            _bus.Publish($"OrderPlaced:{orderId}");
        }
        catch (InvalidOperationException)
        {
            // Perdu : aucune reprise possible.
        }
    }

    public void RelayOutbox()
    {
        // Rien à relayer : l'événement n'a jamais été persisté.
    }
}
