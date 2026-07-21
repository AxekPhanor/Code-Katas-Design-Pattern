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
    public IEnergyResource Create(string kind)
    {
        switch (kind)
        {
            case "solar":
                return new SolarResource();
            case "wind":
                return new WindResource();
            case "nuclear":
                return new NuclearResource();
            default:
                throw new ArgumentException($"Unknown resource kind: {kind}", nameof(kind));
        }
    }
}
