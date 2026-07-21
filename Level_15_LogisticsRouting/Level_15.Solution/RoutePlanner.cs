namespace Level15.Logistics;

/// <summary>Un algorithme d'estimation d'itinéraire, interchangeable.</summary>
public interface IRoutingStrategy
{
    int EstimateMinutes(int distanceKm);
}

public sealed class CarStrategy : IRoutingStrategy
{
    public int EstimateMinutes(int distanceKm) => distanceKm * 1;
}

public sealed class BikeStrategy : IRoutingStrategy
{
    public int EstimateMinutes(int distanceKm) => distanceKm * 4;
}

public sealed class WalkStrategy : IRoutingStrategy
{
    public int EstimateMinutes(int distanceKm) => distanceKm * 12;
}

/// <summary>
/// Le planificateur ne calcule plus rien lui-même : il délègue à la stratégie
/// enregistrée pour le mode. Ajouter un mode = ajouter une stratégie.
/// </summary>
public sealed class RoutePlanner
{
    private readonly Dictionary<string, IRoutingStrategy> _strategies = new()
    {
        ["car"] = new CarStrategy(),
        ["bike"] = new BikeStrategy(),
        ["walk"] = new WalkStrategy(),
    };

    public int Plan(string mode, int distanceKm)
    {
        if (!_strategies.TryGetValue(mode, out var strategy))
        {
            throw new ArgumentException($"Unknown transport mode: {mode}", nameof(mode));
        }

        return strategy.EstimateMinutes(distanceKm);
    }
}
