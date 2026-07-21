using System.Reflection;

using Level11.AccessControl.Tests.Reflection;

using Xunit;

namespace Level11.AccessControl.Tests;

public sealed class ArchitectureTests
{
    private static readonly Assembly Sut = PatternInspector.SystemUnderTest;

    [Fact]
    public void A_substitute_stands_in_front_of_the_real_repository()
    {
        Assert.True(
            PatternInspector.HasProxy(Sut, typeof(IUserRepository)),
            "Un substitut devrait présenter IUserRepository ET envelopper une autre " +
            "instance d'IUserRepository (le sujet réel), afin d'ajouter cache/contrôle " +
            "sans modifier l'appelant.");
    }
}
