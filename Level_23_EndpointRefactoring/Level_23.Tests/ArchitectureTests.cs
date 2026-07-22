using System.Reflection;

using Level23.Endpoints.Tests.Reflection;

using Xunit;

namespace Level23.Endpoints.Tests;

public sealed class ArchitectureTests
{
    private static readonly Assembly Sut = PatternInspector.SystemUnderTest;

    [Fact]
    public void Each_request_lives_in_its_own_endpoint()
    {
        Assert.True(
            PatternInspector.HasRequestEndpointResponse(Sut),
            "Chaque requête devrait être isolée dans son propre point d'entrée dédié " +
            "(une requête -> une réponse), au lieu d'être noyée dans un service " +
            "fourre-tout.");
    }
}
