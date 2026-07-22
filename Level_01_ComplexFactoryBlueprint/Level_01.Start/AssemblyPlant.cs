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

        var builder = new AssemblyFactoryBuilder();

        foreach (var conveyor in conveyors)
        {
            builder.AddConveyor(conveyor.Name, conveyor.UnitsPerMinute);
        }

        if (specification.HasQualityControl)
        {
            builder.AddQualityControl();
        }

        return builder
            .AddRoboticArms(specification.RoboticArms)
            .Build(specification.Name);
    }
}
