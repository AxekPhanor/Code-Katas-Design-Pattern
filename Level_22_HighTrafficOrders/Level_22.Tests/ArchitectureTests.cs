using System.Reflection;

using Level22.Orders.Tests.Reflection;

using Xunit;

namespace Level22.Orders.Tests;

public sealed class ArchitectureTests
{
    private static readonly Assembly Sut = PatternInspector.SystemUnderTest;

    [Fact]
    public void Reads_and_writes_are_handled_by_separate_models()
    {
        Assert.True(
            PatternInspector.HasCommandQuerySeparation(Sut),
            "Le chemin d'écriture (commande) et le chemin de lecture (requête) " +
            "devraient être servis par des gestionnaires distincts, chacun avec son " +
            "modèle, au lieu d'un service unique partagé.");
    }
}
