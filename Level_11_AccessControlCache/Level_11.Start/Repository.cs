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
public static class UserAccess
{
    public static IUserRepository Wrap(DatabaseUserRepository database) => database;
}
