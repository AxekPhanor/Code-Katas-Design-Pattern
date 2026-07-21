using System.Reflection;

using Level06.Organization.Tests.Reflection;

using Xunit;

namespace Level06.Organization.Tests;

public sealed class ArchitectureTests
{
    private static readonly Assembly Sut = PatternInspector.SystemUnderTest;

    [Fact]
    public void Individuals_and_teams_share_one_uniform_abstraction()
    {
        Assert.True(
            PatternInspector.HasCompositeStructure(Sut),
            "Individus et équipes devraient partager une même abstraction, et le " +
            "groupe devrait contenir une collection de cette abstraction — afin " +
            "de supprimer les tests de type (`is Employee` / `is Team`).");
    }
}
