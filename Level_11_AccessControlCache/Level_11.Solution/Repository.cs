namespace Level11.AccessControl;

/// <summary>Contrat stable d'accès aux utilisateurs.</summary>
public interface IUserRepository
{
    string GetUserName(int id);
}

/// <summary>L'accès réel à la base, coûteux.</summary>
public sealed class DatabaseUserRepository : IUserRepository
{
    public int QueryCount { get; private set; }

    public string GetUserName(int id)
    {
        QueryCount++;
        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id), "User id must be positive.");
        }

        return $"User#{id}";
    }
}

/// <summary>
/// Substitut : même contrat que la base, mais met en cache les résultats. Les
/// lectures répétées ne frappent plus la base. L'appelant ne voit aucune
/// différence.
/// </summary>
public sealed class CachingUserRepository : IUserRepository
{
    private readonly IUserRepository _inner;
    private readonly Dictionary<int, string> _cache = new();

    public CachingUserRepository(IUserRepository inner) => _inner = inner;

    public string GetUserName(int id)
    {
        if (_cache.TryGetValue(id, out var cached))
        {
            return cached;
        }

        var name = _inner.GetUserName(id);
        _cache[id] = name;
        return name;
    }
}

public static class UserAccess
{
    public static IUserRepository Wrap(DatabaseUserRepository database) =>
        new CachingUserRepository(database);
}
