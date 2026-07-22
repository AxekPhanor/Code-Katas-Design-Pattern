using System.Reflection;

using Level19.Taxation.Tests.Reflection;

using Xunit;

namespace Level19.Taxation.Tests;

public sealed class ArchitectureTests
{
    private static readonly Assembly Sut = PatternInspector.SystemUnderTest;

    [Fact]
    public void Operations_visit_the_hierarchy_instead_of_switching_on_type()
    {
        Assert.True(
            PatternInspector.HasVisitor(Sut),
            "Une opération devrait être un visiteur (une méthode par type d'élément) " +
            "que les éléments acceptent, afin d'ajouter des opérations sans modifier " +
            "la hiérarchie ni recourir à un switch de type.");
    }
}
