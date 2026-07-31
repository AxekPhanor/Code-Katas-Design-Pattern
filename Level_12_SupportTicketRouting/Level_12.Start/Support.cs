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
        var level1 = new Level1Handler();
        var level2 = new Level2Handler();
        var level3 = new Level3Handler();

        level1.SetNext(level2).SetNext(level3);

        return level1.Handle(ticket);
    }
}

/// <summary>
/// Contrat d'un maillon de la chaîne : sait traiter un ticket ou le déléguer
/// à son successeur.
/// </summary>
public interface ISupportHandler
{
    ISupportHandler SetNext(ISupportHandler next);
    string Handle(Ticket ticket);
}

/// <summary>
/// Comportement commun à tous les maillons : détenir le successeur et savoir
/// déléguer / lever une erreur si personne ne peut traiter.
/// </summary>
public abstract class SupportHandler : ISupportHandler
{
    private ISupportHandler? _next;

    public ISupportHandler SetNext(ISupportHandler next)
    {
        _next = next;
        return next;
    }

    public string Handle(Ticket ticket)
    {
        if (CanHandle(ticket))
        {
            return Respond();
        }

        if (_next is null)
        {
            throw new InvalidOperationException("No support tier can handle this ticket.");
        }

        return _next.Handle(ticket);
    }

    protected abstract bool CanHandle(Ticket ticket);
    protected abstract string Respond();
}

public sealed class Level1Handler : SupportHandler
{
    protected override bool CanHandle(Ticket ticket) => ticket.Difficulty <= 1;
    protected override string Respond() => "Level 1 Support";
}

public sealed class Level2Handler : SupportHandler
{
    protected override bool CanHandle(Ticket ticket) => ticket.Difficulty <= 2;
    protected override string Respond() => "Level 2 Support";
}

public sealed class Level3Handler : SupportHandler
{
    protected override bool CanHandle(Ticket ticket) => ticket.Difficulty <= 3;
    protected override string Respond() => "Level 3 Support";
}