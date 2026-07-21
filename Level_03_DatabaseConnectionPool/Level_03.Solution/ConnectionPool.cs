namespace Level03.Persistence;

/// <summary>
/// Un pool de connexions unique et partagé. Le constructeur est privé : on ne
/// peut plus en créer d'autre. L'unique instance est créée paresseusement et de
/// façon thread-safe (une seule construction, même sous accès concurrent).
/// </summary>
public sealed class ConnectionPool
{
    private static int _constructionCount;

    private static readonly Lazy<ConnectionPool> Instance =
        new(() => new ConnectionPool());

    private ConnectionPool()
    {
        Interlocked.Increment(ref _constructionCount);
    }

    public static int ConstructionCount => _constructionCount;

    public static ConnectionPool GetInstance() => Instance.Value;
}
