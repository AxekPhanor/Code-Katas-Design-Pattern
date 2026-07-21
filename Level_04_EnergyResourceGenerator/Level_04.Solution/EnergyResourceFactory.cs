namespace Level04.EnergyGrid;

/// <summary>
/// Fabrique abstraite : chaque type de ressource a sa propre fabrique concrète.
/// Ajouter un type = ajouter une classe, sans toucher au code existant.
/// </summary>
public abstract class EnergyResourceFactory
{
    public abstract IEnergyResource Create();
}

public sealed class SolarFactory : EnergyResourceFactory
{
    public override IEnergyResource Create() => new SolarResource();
}

public sealed class WindFactory : EnergyResourceFactory
{
    public override IEnergyResource Create() => new WindResource();
}

public sealed class NuclearFactory : EnergyResourceFactory
{
    public override IEnergyResource Create() => new NuclearResource();
}
