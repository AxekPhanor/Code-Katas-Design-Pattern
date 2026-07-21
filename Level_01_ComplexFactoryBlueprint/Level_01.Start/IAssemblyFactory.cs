namespace Level01.AssemblyPlant;

/// <summary>
/// Contrat stable d'une usine d'assemblage assemblée.
/// C'est la surface publique observée par les tests : ne la casse pas pendant
/// ta refactorisation.
/// </summary>
public interface IAssemblyFactory
{
    /// <summary>Nom de la ligne de production.</summary>
    string Name { get; }

    /// <summary>Les convoyeurs installés, dans l'ordre.</summary>
    IReadOnlyList<Conveyor> Conveyors { get; }

    /// <summary>Nombre de bras robotisés.</summary>
    int RoboticArms { get; }

    /// <summary>Indique si une station de contrôle qualité est présente.</summary>
    bool HasQualityControl { get; }

    /// <summary>
    /// Débit estimé de la ligne : limité par le convoyeur le plus lent
    /// (le goulot d'étranglement), augmenté par les bras robotisés.
    /// </summary>
    int EstimatedThroughput();
}
