namespace Level08.Payments;

/// <summary>Ce que le NOUVEAU système attend (contrat cible, stable).</summary>
public interface IPaymentProcessor
{
    PaymentReceipt Pay(decimal amount, string currency);
}

public sealed record PaymentReceipt(bool Success, string Reference);

/// <summary>
/// La VIEILLE API bancaire, incompatible et non modifiable : centimes en
/// entier, code devise, et un statut numérique. On doit faire avec.
/// </summary>
public sealed class LegacyBankApi
{
    public LegacyResponse Charge(long amountInCents, string currencyCode)
    {
        if (amountInCents <= 0)
        {
            return new LegacyResponse(2, string.Empty);
        }

        return new LegacyResponse(0, $"TXN-{currencyCode}-{amountInCents}");
    }
}

public sealed record LegacyResponse(int Status, string Reference);

// -----------------------------------------------------------------------------
//  CODE MÉTIER À REFACTORISER
// -----------------------------------------------------------------------------
//  Ce processeur implémente le contrat cible, mais mélange TOUT : il fabrique
//  la vieille API à la volée et éparpille les conversions (euros -> centimes,
//  statut -> booléen) au cœur de la méthode. L'ancienne API et la nouvelle sont
//  soudées ; impossible de réutiliser proprement cette traduction ailleurs.
//
//  Objectif : isoler la traduction dans un composant dédié qui enveloppe la
//  vieille API et expose le contrat cible.
// -----------------------------------------------------------------------------
public sealed class TangledPaymentProcessor : IPaymentProcessor
{
    public PaymentReceipt Pay(decimal amount, string currency)
    {
        var legacy = new LegacyBankApi();
        var cents = (long)(amount * 100);
        var response = legacy.Charge(cents, currency);
        return new PaymentReceipt(response.Status == 0, response.Reference);
    }
}

public static class PaymentGateway
{
    public static IPaymentProcessor Create() => new TangledPaymentProcessor();
}
