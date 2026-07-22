namespace Level02.Bestiary;

// -----------------------------------------------------------------------------
//  CODE MÉTIER À REFACTORISER
// -----------------------------------------------------------------------------
//  Le spawner est configuré avec un ennemi "modèle", mais pour produire chaque
//  copie il doit le RECONSTRUIRE champ par champ. Il connaît donc tous les
//  détails de construction d'Enemy : si l'ennemi gagne un nouvel attribut
//  (armure, résistances, butin…), ce code casse, et chaque nouveau type
//  d'ennemi force à réécrire la logique de copie.
//
//  Objectif : l'ennemi modèle devrait savoir se dupliquer lui-même, pour que
//  le spawner n'ait plus rien à connaître de sa construction.
// -----------------------------------------------------------------------------
public sealed class EnemySpawner
{
    private readonly IEnemy _prototype;

    public EnemySpawner(Enemy prototype)
    {
        _prototype = prototype;
    }

    public IEnemy Spawn()
    {
        return _prototype.Clone();
    }
}
