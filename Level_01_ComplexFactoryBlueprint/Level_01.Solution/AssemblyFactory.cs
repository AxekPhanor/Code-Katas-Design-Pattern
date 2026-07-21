namespace Level01.AssemblyPlant;

/// <summary>
/// Usine d'assemblage immuable. Son constructeur est <c>internal</c> :
/// on ne peut plus la fabriquer à la main via un constructeur géant, elle
/// s'obtient exclusivement par assemblage progressif.
/// </summary>
public sealed class AssemblyFactory : IAssemblyFactory
{
    private readonly IReadOnlyList<Conveyor> _conveyors;

    internal AssemblyFactory(
        string name,
        IReadOnlyList<Conveyor> conveyors,
        int roboticArms,
        bool hasQualityControl)
    {
        Name = name;
        _conveyors = conveyors;
        RoboticArms = roboticArms;
        HasQualityControl = hasQualityControl;
    }

    public string Name { get; }

    public IReadOnlyList<Conveyor> Conveyors => _conveyors;

    public int RoboticArms { get; }

    public bool HasQualityControl { get; }

    public int EstimatedThroughput()
    {
        if (_conveyors.Count == 0)
        {
            return 0;
        }

        var bottleneck = _conveyors.Min(conveyor => conveyor.UnitsPerMinute);
        return bottleneck + (RoboticArms * 5);
    }
}
