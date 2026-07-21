namespace Level02.Bestiary;

/// <summary>
/// Contrat stable d'un ennemi.
/// </summary>
public interface IEnemy
{
    string Name { get; }
    int Health { get; }
    int Damage { get; }
    IReadOnlyList<string> Abilities { get; }
    void TakeDamage(int amount);
}

/// <summary>
/// Un ennemi capable de se dupliquer lui-même : la connaissance de sa propre
/// construction reste chez lui, plus chez le spawner.
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

    public string Name { get; }

    public int Health { get; private set; }

    public int Damage { get; }

    public IReadOnlyList<string> Abilities => _abilities;

    public void TakeDamage(int amount)
    {
        Health = Math.Max(0, Health - amount);
    }

    /// <summary>Produit une copie indépendante de cet ennemi.</summary>
    public IEnemy Clone() => new Enemy(Name, Health, Damage, _abilities);
}
