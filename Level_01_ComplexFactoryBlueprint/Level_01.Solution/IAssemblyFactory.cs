namespace Level01.AssemblyPlant;

/// <summary>
/// Contrat stable d'une usine d'assemblage assemblée.
/// </summary>
public interface IAssemblyFactory
{
    string Name { get; }

    IReadOnlyList<Conveyor> Conveyors { get; }

    int RoboticArms { get; }

    bool HasQualityControl { get; }

    int EstimatedThroughput();
}
