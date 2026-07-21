namespace Level02.Bestiary;

/// <summary>
/// Le spawner ne connaît plus rien de la construction d'un ennemi : il demande
/// simplement au modèle de se dupliquer. Ajouter un attribut ou un nouveau type
/// d'ennemi n'impacte plus ce code.
/// </summary>
public sealed class EnemySpawner
{
    private readonly Enemy _prototype;

    public EnemySpawner(Enemy prototype)
    {
        _prototype = prototype;
    }

    public IEnemy Spawn() => _prototype.Clone();
}
