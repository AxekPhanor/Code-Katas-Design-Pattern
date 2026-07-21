namespace Level04.EnergyGrid;

/// <summary>Contrat stable d'une ressource énergétique.</summary>
public interface IEnergyResource
{
    string Kind { get; }
    int OutputMegawatts { get; }
}

public sealed class SolarResource : IEnergyResource
{
    public string Kind => "solar";
    public int OutputMegawatts => 5;
}

public sealed class WindResource : IEnergyResource
{
    public string Kind => "wind";
    public int OutputMegawatts => 3;
}

public sealed class NuclearResource : IEnergyResource
{
    public string Kind => "nuclear";
    public int OutputMegawatts => 1000;
}
