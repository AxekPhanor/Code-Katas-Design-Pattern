namespace Level01.AssemblyPlant;

/// <summary>
/// Décrit l'usine que le client souhaite obtenir. C'est l'entrée du
/// point d'assemblage (<see cref="AssemblyPlant.Assemble"/>).
/// </summary>
public sealed class FactorySpecification
{
    public string Name { get; set; } = string.Empty;

    public IReadOnlyList<Conveyor> Conveyors { get; set; } = Array.Empty<Conveyor>();

    public int RoboticArms { get; set; }

    public bool HasQualityControl { get; set; }
}
