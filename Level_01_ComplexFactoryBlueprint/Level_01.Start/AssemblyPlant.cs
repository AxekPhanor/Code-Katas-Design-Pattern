namespace Level01.AssemblyPlant;

// -----------------------------------------------------------------------------
//  POINT D'ENTRÉE STABLE (utilisé par les tests) — garde cette signature.
// -----------------------------------------------------------------------------
//  Regarde à quel point implémenter ce point d'entrée est pénible aujourd'hui :
//  il faut aplatir la spécification dans le constructeur géant, jongler avec des
//  emplacements de convoyeurs nuls, et refuser purement et simplement une usine
//  qui aurait plus de 3 convoyeurs.
//
//  À toi de rendre cet assemblage propre et extensible.
// -----------------------------------------------------------------------------
public static class AssemblyPlant
{
    public static IAssemblyFactory Assemble(FactorySpecification specification)
    {
        ArgumentNullException.ThrowIfNull(specification);

        var conveyors = specification.Conveyors;

        if (conveyors.Count == 0)
        {
            throw new ArgumentException(
                "At least one conveyor is required.", nameof(specification));
        }

        if (conveyors.Count > 3)
        {
            throw new NotSupportedException(
                "This assembly line is hard-wired for a maximum of 3 conveyors.");
        }

        return new AssemblyFactory(
            specification.Name,
            conveyors[0].Name, conveyors[0].UnitsPerMinute,
            conveyors.Count > 1 ? conveyors[1].Name : null, conveyors.Count > 1 ? conveyors[1].UnitsPerMinute : 0,
            conveyors.Count > 2 ? conveyors[2].Name : null, conveyors.Count > 2 ? conveyors[2].UnitsPerMinute : 0,
            specification.RoboticArms,
            specification.HasQualityControl,
            packagingUnit: false);
    }
}
