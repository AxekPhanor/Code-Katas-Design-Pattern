using System.Reflection;

using Level12.Support.Tests.Reflection;

using Xunit;

namespace Level12.Support.Tests;

public sealed class ArchitectureTests
{
    private static readonly Assembly Sut = PatternInspector.SystemUnderTest;

    [Fact]
    public void Tickets_travel_along_a_chain_of_autonomous_handlers()
    {
        Assert.True(
            PatternInspector.HasHandlerChain(Sut),
            "L'escalade devrait suivre une chaîne : une abstraction de gestionnaire " +
            "dont chaque maillon référence son successeur, au lieu d'une cascade de " +
            "if/else centralisée.");
    }
}
