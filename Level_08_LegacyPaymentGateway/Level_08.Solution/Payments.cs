namespace Level08.Payments;

/// <summary>Ce que le NOUVEAU système attend (contrat cible, stable).</summary>
public interface IPaymentProcessor
{
    PaymentReceipt Pay(decimal amount, string currency);
}

public sealed record PaymentReceipt(bool Success, string Reference);

/// <summary>La vieille API bancaire, incompatible et non modifiable.</summary>
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

/// <summary>
/// Adaptateur : enveloppe la vieille API et expose le contrat cible. Toute la
/// traduction (euros -> centimes, statut -> booléen) est isolée ici.
/// </summary>
public sealed class LegacyBankAdapter : IPaymentProcessor
{
    private readonly LegacyBankApi _legacy;

    public LegacyBankAdapter(LegacyBankApi legacy) => _legacy = legacy;

    public PaymentReceipt Pay(decimal amount, string currency)
    {
        var cents = (long)(amount * 100);
        var response = _legacy.Charge(cents, currency);
        return new PaymentReceipt(response.Status == 0, response.Reference);
    }
}

public static class PaymentGateway
{
    public static IPaymentProcessor Create() => new LegacyBankAdapter(new LegacyBankApi());
}
