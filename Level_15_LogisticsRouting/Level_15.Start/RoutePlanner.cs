namespace Level15.Logistics;

// -----------------------------------------------------------------------------
//  CODE MÉTIER À REFACTORISER
// -----------------------------------------------------------------------------
//  Le choix de l'algorithme d'itinéraire repose sur un `switch` sur le mode de
//  transport, avec le calcul codé en dur dans chaque branche. Ajouter un mode
//  (train, trottinette, cargo…) oblige à rouvrir cette méthode. Les algorithmes
//  ne sont pas interchangeables ni testables isolément.
//
//  Objectif : rendre chaque algorithme d'estimation autonome et interchangeable,
//  et laisser le planificateur déléguer à la stratégie choisie.
// -----------------------------------------------------------------------------
public sealed class RoutePlanner
{
    public int Plan(string mode, int distanceKm)
    {
        switch (mode)
        {
            case "car":
                return distanceKm * 1;
            case "bike":
                return distanceKm * 4;
            case "walk":
                return distanceKm * 12;
            default:
                throw new ArgumentException($"Unknown transport mode: {mode}", nameof(mode));
        }
    }
}
