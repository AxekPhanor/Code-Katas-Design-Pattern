namespace Level21.Composition;

/// <summary>Un registre de services minimal (par nom).</summary>
public sealed class ServiceRegistry
{
    private readonly HashSet<string> _services = new();

    public void Add(string service) => _services.Add(service);

    public bool Contains(string service) => _services.Contains(service);

    public int Count => _services.Count;
}

// -----------------------------------------------------------------------------
//  CODE MÉTIER À REFACTORISER
// -----------------------------------------------------------------------------
//  Tous les enregistrements de services vivent dans une seule méthode obèse.
//  Elle grossit sans fin, mélange des domaines sans rapport (paiement,
//  notifications, commandes) et devient un point de conflit permanent entre
//  équipes.
//
//  Objectif : découper cet enregistrement en modules cohérents, chacun exposé
//  par une méthode d'extension sur le registre, composées par le démarrage.
// -----------------------------------------------------------------------------
public static class Startup
{
    public static void ConfigureServices(ServiceRegistry services)
    {
        services.Add("PaymentGateway");
        services.Add("RefundService");
        services.Add("EmailSender");
        services.Add("SmsSender");
        services.Add("OrderRepository");
        services.Add("OrderValidator");
    }
}
