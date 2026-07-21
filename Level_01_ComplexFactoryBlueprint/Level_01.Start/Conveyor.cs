namespace Level01.AssemblyPlant;

/// <summary>
/// Un convoyeur d'une ligne d'assemblage, caractérisé par son nom et sa
/// cadence (nombre d'unités traitées par minute).
/// </summary>
public sealed class Conveyor
{
    public Conveyor(string name, int unitsPerMinute)
    {
        Name = name;
        UnitsPerMinute = unitsPerMinute;
    }

    public string Name { get; }

    public int UnitsPerMinute { get; }
}
