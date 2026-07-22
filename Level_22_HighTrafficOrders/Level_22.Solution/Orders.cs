namespace Level22.Orders;

/// <summary>Vue de lecture d'une commande (contrat stable).</summary>
public sealed record OrderSummary(string OrderId, decimal Amount, string Status);

// Modèle d'ÉCRITURE (commande) et modèle de LECTURE (requête), distincts.
public sealed record PlaceOrderCommand(string OrderId, decimal Amount);

public sealed record OrderSummaryQuery(string OrderId);

/// <summary>Gestionnaire du chemin d'écriture.</summary>
public interface IOrderCommandHandler
{
    void Handle(PlaceOrderCommand command);
}

/// <summary>Gestionnaire du chemin de lecture.</summary>
public interface IOrderQueryHandler
{
    OrderSummary Handle(OrderSummaryQuery query);
}

internal sealed class OrderStore
{
    public Dictionary<string, decimal> Orders { get; } = new();
}

public sealed class PlaceOrderCommandHandler : IOrderCommandHandler
{
    private readonly OrderStore _store;

    internal PlaceOrderCommandHandler(OrderStore store) => _store = store;

    public void Handle(PlaceOrderCommand command) => _store.Orders[command.OrderId] = command.Amount;
}

public sealed class OrderSummaryQueryHandler : IOrderQueryHandler
{
    private readonly OrderStore _store;

    internal OrderSummaryQueryHandler(OrderStore store) => _store = store;

    public OrderSummary Handle(OrderSummaryQuery query)
    {
        var amount = _store.Orders[query.OrderId];
        return new OrderSummary(query.OrderId, amount, amount >= 1000m ? "Priority" : "Standard");
    }
}

/// <summary>
/// Point d'entrée : les deux chemins sont servis par des gestionnaires séparés,
/// chacun avec son modèle. On peut faire évoluer lecture et écriture séparément.
/// </summary>
public sealed class OrderApi
{
    private readonly IOrderCommandHandler _commandHandler;
    private readonly IOrderQueryHandler _queryHandler;

    public OrderApi()
    {
        var store = new OrderStore();
        _commandHandler = new PlaceOrderCommandHandler(store);
        _queryHandler = new OrderSummaryQueryHandler(store);
    }

    public void Place(string orderId, decimal amount) =>
        _commandHandler.Handle(new PlaceOrderCommand(orderId, amount));

    public OrderSummary GetSummary(string orderId) =>
        _queryHandler.Handle(new OrderSummaryQuery(orderId));
}
