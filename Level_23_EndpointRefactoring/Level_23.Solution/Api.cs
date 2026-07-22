namespace Level23.Endpoints;

/// <summary>Requête et réponse explicites, propres à cette opération.</summary>
public sealed record RegisterUserRequest(string Email, string Password);

public sealed record RegisterUserResponse(string UserId, bool Success);

/// <summary>Un point d'entrée : une requête -> une réponse.</summary>
public interface IEndpoint<TRequest, TResponse>
{
    TResponse Handle(TRequest request);
}

/// <summary>
/// Point d'entrée dédié à l'inscription, isolé de bout en bout. Chaque opération
/// vit dans sa propre classe, avec ses propres types d'entrée/sortie.
/// </summary>
public sealed class RegisterUserEndpoint : IEndpoint<RegisterUserRequest, RegisterUserResponse>
{
    public RegisterUserResponse Handle(RegisterUserRequest request)
    {
        var success = request.Email.Contains('@') && request.Password.Length >= 8;
        return new RegisterUserResponse(success ? $"user-{request.Email}" : string.Empty, success);
    }
}

public static class Api
{
    public static RegisterUserResponse RegisterUser(string email, string password) =>
        new RegisterUserEndpoint().Handle(new RegisterUserRequest(email, password));
}
