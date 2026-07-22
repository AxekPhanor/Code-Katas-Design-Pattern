namespace Level03.Persistence;

// -----------------------------------------------------------------------------
//  CODE MÉTIER À REFACTORISER
// -----------------------------------------------------------------------------
//  Ce pool de connexions est censé être une ressource UNIQUE et partagée par
//  toute l'application. Or :
//
//   * son constructeur est public → n'importe qui peut en créer un deuxième ;
//   * `GetInstance()` fabrique une NOUVELLE instance à chaque appel → chaque
//     partie du code se retrouve avec son propre pool, les connexions ne sont
//     donc jamais réellement partagées, et l'accès concurrent n'est pas maîtrisé.
//
//  Objectif : garantir qu'il n'existe qu'une seule instance, protégée et
//  partagée, y compris en multithread.
// -----------------------------------------------------------------------------
public sealed class ConnectionPool
{
    private static int _constructionCount;

    public static ConnectionPool Instance { get; } = new ConnectionPool();

    private ConnectionPool()
    {
        Interlocked.Increment(ref _constructionCount);
    }

    /// <summary>Nombre total d'instances réellement construites.</summary>
    public static int ConstructionCount => _constructionCount;

    public static ConnectionPool GetInstance()
    {
        // Bug : une nouvelle instance à chaque appel.
        return Instance;
    }
}
