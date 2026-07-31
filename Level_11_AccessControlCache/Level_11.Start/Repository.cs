namespace Level11.AccessControl;

/// <summary>Contrat stable d'accès aux utilisateurs.</summary>
public interface IUserRepository
{
    string GetUserName(int id);
}

/// <summary>
/// L'accès réel à la base : chaque lecture est un aller-retour coûteux, compté
/// par <see cref="QueryCount"/>. On ne modifie pas cette classe.
/// </summary>
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

// -----------------------------------------------------------------------------
//  CODE MÉTIER À REFACTORISER
// -----------------------------------------------------------------------------
//  Le point d'accès rend directement la base : aucune couche ne s'intercale pour
//  mettre en cache ou contrôler les accès. Chaque lecture — même répétée pour le
//  même identifiant — frappe la base.
//
//  Objectif : intercaler un substitut qui présente le MÊME contrat, mais met en
//  cache les résultats (et pourrait contrôler les accès) sans changer l'appelant.
// -----------------------------------------------------------------------------
/// <summary>
/// Substitut (Proxy) : présente le même contrat que le sujet réel, mais met en
/// cache les résultats pour éviter les allers-retours répétés. L'appelant ne
/// voit aucune différence.
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