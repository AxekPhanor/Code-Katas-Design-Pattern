namespace Level12.Support;

/// <summary>Un ticket technique, avec un niveau de difficulté.</summary>
public sealed record Ticket(string Title, int Difficulty);

/// <summary>
/// Maillon de la chaîne : chaque gestionnaire traite le ticket s'il en est
/// capable, sinon il le transmet à son successeur. Les paliers deviennent des
/// unités autonomes, réordonnables.
/// </summary>
public abstract class SupportHandler
{
    private SupportHandler? _next;

    public SupportHandler SetNext(SupportHandler next)
    {
        _next = next;
        return next;
    }

    public string Handle(Ticket ticket)
    {
        if (CanHandle(ticket))
        {
            return Label;
        }

        if (_next is not null)
        {
            return _next.Handle(ticket);
        }

        throw new InvalidOperationException("No support tier can handle this ticket.");
    }

    protected abstract bool CanHandle(Ticket ticket);

    protected abstract string Label { get; }
}

public sealed class Level1Support : SupportHandler
{
    protected override bool CanHandle(Ticket ticket) => ticket.Difficulty <= 1;
    protected override string Label => "Level 1 Support";
}

public sealed class Level2Support : SupportHandler
{
    protected override bool CanHandle(Ticket ticket) => ticket.Difficulty <= 2;
    protected override string Label => "Level 2 Support";
}

public sealed class Level3Support : SupportHandler
{
    protected override bool CanHandle(Ticket ticket) => ticket.Difficulty <= 3;
    protected override string Label => "Level 3 Support";
}

public static class SupportDesk
{
    public static string Route(Ticket ticket)
    {
        var level1 = new Level1Support();
        var level2 = new Level2Support();
        var level3 = new Level3Support();

        level1.SetNext(level2).SetNext(level3);

        return level1.Handle(ticket);
    }
}
