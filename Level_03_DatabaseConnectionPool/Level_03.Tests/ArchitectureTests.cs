using Level03.Persistence.Tests.Reflection;

using Xunit;

namespace Level03.Persistence.Tests;

/// <summary>
/// Vérifie que le bon patron a été appliqué, sans imposer de noms de classes.
/// </summary>
public sealed class ArchitectureTests
{
    [Fact]
    public void The_pool_is_a_single_guarded_shared_instance()
    {
        Assert.True(
            PatternInspector.IsSingleGuardedInstance(typeof(ConnectionPool)),
            "L'accès au pool devrait passer par une unique instance protégée : " +
            "aucun constructeur public, et un accesseur statique renvoyant le pool.");
    }
}
