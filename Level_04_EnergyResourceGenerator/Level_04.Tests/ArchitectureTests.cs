using System.Reflection;

using Level04.EnergyGrid.Tests.Reflection;

using Xunit;

namespace Level04.EnergyGrid.Tests;

public sealed class ArchitectureTests
{
    private static readonly Assembly Sut = PatternInspector.SystemUnderTest;

    [Fact]
    public void Resources_are_created_through_dedicated_factories()
    {
        Assert.True(
            PatternInspector.HasFactoryMethodHierarchy(Sut, typeof(IEnergyResource)),
            "La création devrait passer par une hiérarchie de fabriques : une " +
            "classe abstraite déclarant une méthode de création d'IEnergyResource, " +
            "redéfinie par au moins deux fabriques concrètes (au lieu d'un switch).");
    }
}
