namespace Level12.Support;

/// <summary>Un ticket technique, avec un niveau de difficulté.</summary>
public sealed record Ticket(string Title, int Difficulty);

// -----------------------------------------------------------------------------
//  CODE MÉTIER À REFACTORISER
// -----------------------------------------------------------------------------
//  L'escalade repose sur une cascade de `if/else` centralisée : ce point unique
//  connaît TOUS les niveaux de support et l'ordre d'escalade. Insérer un palier
//  (ou changer une règle) oblige à rouvrir cette méthode. Les paliers ne sont pas
//  des unités autonomes qu'on pourrait réordonner ou réutiliser.
//
//  Objectif : faire circuler le ticket le long d'une chaîne de gestionnaires
//  autonomes, chacun décidant de le traiter ou de le passer au suivant.
// -----------------------------------------------------------------------------
public static class SupportDesk
{
    public static string Route(Ticket ticket)
    {
        if (ticket.Difficulty <= 1)
        {
            return "Level 1 Support";
        }
        else if (ticket.Difficulty <= 2)
        {
            return "Level 2 Support";
        }
        else if (ticket.Difficulty <= 3)
        {
            return "Level 3 Support";
        }
        else
        {
            throw new InvalidOperationException("No support tier can handle this ticket.");
        }
    }
}
