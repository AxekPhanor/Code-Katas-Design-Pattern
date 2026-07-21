using System.Reflection;

using Level05.Ui.Tests.Reflection;

using Xunit;

namespace Level05.Ui.Tests;

public sealed class ArchitectureTests
{
    private static readonly Assembly Sut = PatternInspector.SystemUnderTest;

    [Fact]
    public void Coherent_component_families_come_from_a_dedicated_factory()
    {
        Assert.True(
            PatternInspector.HasAbstractFactory(Sut),
            "Une fabrique dédiée devrait produire la famille complète de composants " +
            "d'une plateforme (au moins deux méthodes renvoyant des contrats distincts), " +
            "avec une implémentation par plateforme.");
    }
}
