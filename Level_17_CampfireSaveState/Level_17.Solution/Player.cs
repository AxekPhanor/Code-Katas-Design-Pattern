namespace Level17.Campfire;

/// <summary>
/// Jeton de sauvegarde OPAQUE : son état n'est visible qu'en interne (le joueur),
/// aucun membre public modifiable. Impossible de le lire ou de le forger de
/// l'extérieur.
/// </summary>
public sealed class PlayerMemento
{
    internal int Health { get; }
    internal int Position { get; }

    internal PlayerMemento(int health, int position)
    {
        Health = health;
        Position = position;
    }
}

public sealed class Player
{
    public int Health { get; private set; } = 100;
    public int Position { get; private set; }

    public void TakeDamage(int amount) => Health = Math.Max(0, Health - amount);

    public void Move(int steps) => Position += steps;

    public PlayerMemento Save() => new(Health, Position);

    public void Restore(PlayerMemento memento)
    {
        Health = memento.Health;
        Position = memento.Position;
    }
}
