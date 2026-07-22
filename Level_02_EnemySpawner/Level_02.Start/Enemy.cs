namespace Level02.Bestiary;

/// <summary>
/// Contrat stable d'un ennemi. Ne le casse pas pendant ta refactorisation.
/// </summary>
public interface IEnemy
{
    string Name { get; }
    int Health { get; }
    int Damage { get; }
    IReadOnlyList<string> Abilities { get; }
    void TakeDamage(int amount);
    
    IEnemy Clone();
}

/// <summary>
/// Un ennemi concret. Aujourd'hui, il ne sait pas produire une copie de
/// lui-même : c'est le spawner qui doit le reconstruire de toutes pièces
/// (voir <see cref="EnemySpawner"/>).
/// </summary>
public sealed class Enemy : IEnemy
{
    private readonly List<string> _abilities;

    public Enemy(string name, int health, int damage, IReadOnlyList<string> abilities)
    {
        Name = name;
        Health = health;
        Damage = damage;
        _abilities = abilities.ToList();
    }

    public Enemy(Enemy copy): this(copy.Name, copy.Health, copy.Damage, copy.Abilities) {}

    public string Name { get; }

    public int Health { get; private set; }

    public int Damage { get; }
    
    public IReadOnlyList<string> Abilities => _abilities;

    public void TakeDamage(int amount)
    {
        Health = Math.Max(0, Health - amount);
    }

    public IEnemy Clone()
    {
        return new Enemy(this);
    }
}
