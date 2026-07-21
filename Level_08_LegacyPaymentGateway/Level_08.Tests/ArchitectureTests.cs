using System.Reflection;

using Level08.Payments.Tests.Reflection;

using Xunit;

namespace Level08.Payments.Tests;

public sealed class ArchitectureTests
{
    private static readonly Assembly Sut = PatternInspector.SystemUnderTest;

    [Fact]
    public void A_dedicated_adapter_wraps_the_legacy_api()
    {
        Assert.True(
            PatternInspector.HasAdapter(Sut, typeof(IPaymentProcessor), typeof(LegacyBankApi)),
            "Un adaptateur dédié devrait ENVELOPPER LegacyBankApi (paramètre de " +
            "constructeur ou champ) tout en exposant IPaymentProcessor, au lieu de " +
            "mélanger la vieille API et la traduction dans une seule méthode.");
    }
}
