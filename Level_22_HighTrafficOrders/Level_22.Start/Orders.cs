namespace Level22.Orders;

/// <summary>Vue de lecture d'une commande (contrat stable).</summary>
public sealed record OrderSummary(string OrderId, decimal Amount, string Status);

// -----------------------------------------------------------------------------
//  CODE MÉTIER À REFACTORISER
// -----------------------------------------------------------------------------
//  Lecture et écriture partagent le MÊME service et le MÊME modèle. Sous forte
//  charge, on ne peut ni optimiser les lectures ni faire évoluer les écritures
//  indépendamment : tout est couplé dans une seule classe, sur un seul modèle.
//
//  Objectif : séparer le chemin d'ÉCRITURE (commandes) du chemin de LECTURE
//  (requêtes), chacun avec son propre modèle et son propre gestionnaire.
// -----------------------------------------------------------------------------
public sealed class OrderApi
{
    private readonly Dictionary<string, decimal> _orders = new();

    public void Place(string orderId, decimal amount) => _orders[orderId] = amount;

    public OrderSummary GetSummary(string orderId)
    {
        var amount = _orders[orderId];
        return new OrderSummary(orderId, amount, amount >= 1000m ? "Priority" : "Standard");
    }
}
