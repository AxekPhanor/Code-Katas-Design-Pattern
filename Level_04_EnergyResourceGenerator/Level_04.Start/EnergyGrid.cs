namespace Level04.EnergyGrid;

// -----------------------------------------------------------------------------
//  CODE MÉTIER À REFACTORISER
// -----------------------------------------------------------------------------
//  L'instanciation des ressources repose sur un gros `switch` centralisé.
//  Chaque nouveau type de ressource oblige à rouvrir cette méthode et à ajouter
//  un `case` : la classe connaît tous les types concrets et grossit sans fin
//  (violation de l'ouverture/fermeture).
//
//  Objectif : pouvoir ajouter un type de ressource sans modifier ce code, en
//  déléguant la création à des fabriques dédiées.
// -----------------------------------------------------------------------------
public sealed class EnergyGrid
{
    private readonly Dictionary<string, EnergyResourceFactory> _factories = new()
    {
        ["solar"] = new SolarResourceFactory(),
        ["wind"] = new WindResourceFactory(),
        ["nuclear"] = new NuclearResourceFactory(),
    };

    public IEnergyResource Create(string kind)
    {
        if (!_factories.TryGetValue(kind, out var factory))
        {
            throw new ArgumentException($"Unknown resource kind: {kind}", nameof(kind));
        }

        return factory.Create();
    }
}
