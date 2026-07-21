namespace Level01.AssemblyPlant;

/// <summary>
/// Point d'entrée stable. L'assemblage est désormais lisible et extensible :
/// on décrit l'usine étape par étape, quel que soit le nombre de convoyeurs.
/// </summary>
public static class AssemblyPlant
{
    public static IAssemblyFactory Assemble(FactorySpecification specification)
    {
        ArgumentNullException.ThrowIfNull(specification);

        var builder = new AssemblyFactoryBuilder()
            .Named(specification.Name);

        foreach (var conveyor in specification.Conveyors)
        {
            builder.AddConveyor(conveyor.Name, conveyor.UnitsPerMinute);
        }

        builder.WithRoboticArms(specification.RoboticArms);

        if (specification.HasQualityControl)
        {
            builder.WithQualityControl();
        }

        return builder.Build();
    }
}
