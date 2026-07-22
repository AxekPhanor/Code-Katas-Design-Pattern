namespace Level23.Endpoints;

/// <summary>Réponse d'inscription (contrat stable).</summary>
public sealed record RegisterUserResponse(string UserId, bool Success);

// -----------------------------------------------------------------------------
//  CODE MÉTIER À REFACTORISER
// -----------------------------------------------------------------------------
//  Un service "fourre-tout" regroupe des opérations métier sans rapport
//  (inscription, connexion, réinitialisation…). Chaque requête complexe est
//  noyée dans une classe géante ; entrées/sorties ne sont pas explicites, et
//  tout changement risque d'impacter des opérations voisines.
//
//  Objectif : isoler CHAQUE requête de bout en bout dans son propre point
//  d'entrée dédié, avec sa requête et sa réponse explicites.
// -----------------------------------------------------------------------------
public sealed class ApiService
{
    public RegisterUserResponse RegisterUser(string email, string password)
    {
        var success = email.Contains('@') && password.Length >= 8;
        return new RegisterUserResponse(success ? $"user-{email}" : string.Empty, success);
    }

    // ... et aussi, entassées ici, toutes les autres opérations :
    public string LoginUser(string email, string password) => "session-token";

    public bool ResetPassword(string email) => email.Contains('@');
}

public static class Api
{
    public static RegisterUserResponse RegisterUser(string email, string password) =>
        new ApiService().RegisterUser(email, password);
}
