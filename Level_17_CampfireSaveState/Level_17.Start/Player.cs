namespace Level17.Campfire;

// -----------------------------------------------------------------------------
//  CODE MÉTIER À REFACTORISER
// -----------------------------------------------------------------------------
//  La sauvegarde expose l'état interne du joueur en clair : `Save` renvoie un
//  objet aux propriétés PUBLIQUES modifiables, que n'importe qui peut lire,
//  forger ou altérer. L'encapsulation du joueur est brisée par le mécanisme de
//  sauvegarde lui-même.
//
//  Objectif : capturer l'état dans un jeton OPAQUE, que seul le joueur sait
//  restaurer, sans exposer ses champs internes.
// -----------------------------------------------------------------------------
public sealed class PlayerState
{
    public int Health { get; set; }
    public int Position { get; set; }
}

public sealed class Player
{
    public int Health { get; private set; } = 100;
    public int Position { get; private set; }

    public void TakeDamage(int amount) => Health = Math.Max(0, Health - amount);

    public void Move(int steps) => Position += steps;

    public PlayerState Save() => new() { Health = Health, Position = Position };

    public void Restore(PlayerState state)
    {
        Health = state.Health;
        Position = state.Position;
    }
}
