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

    public AssemblyFactory(
        string name,
        string conveyor1Name, int conveyor1Speed,
        string? conveyor2Name, int conveyor2Speed,
        string? conveyor3Name, int conveyor3Speed,
        int roboticArms,
        bool qualityControl,
        bool packagingUnit)
    {
        Name = name;

        if (!string.IsNullOrWhiteSpace(conveyor1Name))
        {
            _conveyors.Add(new Conveyor(conveyor1Name, conveyor1Speed));
        }

        if (!string.IsNullOrWhiteSpace(conveyor2Name))
        {
            _conveyors.Add(new Conveyor(conveyor2Name, conveyor2Speed));
        }

        if (!string.IsNullOrWhiteSpace(conveyor3Name))
        {
            _conveyors.Add(new Conveyor(conveyor3Name, conveyor3Speed));
        }

        RoboticArms = roboticArms;
        HasQualityControl = qualityControl;

        // `packagingUnit` : drapeau positionnel hérité, aujourd'hui sans effet.
        // Exactement le genre de paramètre fantôme qui rend ce constructeur
        // illisible et dangereux à l'appel.
        _ = packagingUnit;
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
