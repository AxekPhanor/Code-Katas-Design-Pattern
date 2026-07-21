namespace Level04.EnergyGrid;

/// <summary>
/// Le grid ne connaît plus les types concrets : il délègue la création à la
/// fabrique enregistrée pour ce type. Plus de `switch` à faire grossir.
/// </summary>
public sealed class EnergyGrid
{
    private readonly Dictionary<string, EnergyResourceFactory> _factories = new()
    {
        ["solar"] = new SolarFactory(),
        ["wind"] = new WindFactory(),
        ["nuclear"] = new NuclearFactory(),
    };

    public IEnergyResource Create(string kind)
    {
        if (!_factories.TryGetValue(kind, out var factory))
        {
            throw new ArgumentException($"Unknown resource kind: {kind}", nameof(kind));
        }

        return factory.Create();
    }
}
