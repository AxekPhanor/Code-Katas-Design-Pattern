namespace Level01.AssemblyPlant;

// -----------------------------------------------------------------------------
//  CODE MÉTIER À REFACTORISER
// -----------------------------------------------------------------------------
//  Cette usine "fonctionne", mais elle est douloureuse à construire et à faire
//  évoluer :
//
//   * un constructeur GÉANT et illisible, mélangeant chaînes, entiers et
//     booléens positionnels (impossible de savoir ce que signifie `true, false`
//     à l'appel) ;
//   * un nombre de convoyeurs FIGÉ à 3 emplacements : ajouter un 4e convoyeur
//     demande de changer la signature du constructeur et tout le code appelant ;
//   * des paramètres fantômes (`packagingUnit`) qui alourdissent encore l'appel.
//
//  Objectif : rendre la construction lisible, sûre et extensible à un nombre
//  quelconque de convoyeurs — sans toucher au contrat public IAssemblyFactory.
// -----------------------------------------------------------------------------
public sealed class AssemblyFactory : IAssemblyFactory
{
    private readonly List<Conveyor> _conveyors = new();

    public AssemblyFactory(string name, AssemblyFactoryBuilder builder)
    {
        Name = name;
        _conveyors = builder.conveyors.ToList();
        RoboticArms = builder.roboticArms;
        HasQualityControl = builder.qualityControl;
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

        var bottleneck = int.MaxValue;
        foreach (var conveyor in _conveyors)
        {
            if (conveyor.UnitsPerMinute < bottleneck)
            {
                bottleneck = conveyor.UnitsPerMinute;
            }
        }

        return bottleneck + (RoboticArms * 5);
    }
}
