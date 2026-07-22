using System.Reflection;

using Level26.Resilience.Tests.Reflection;

using Xunit;

namespace Level26.Resilience.Tests;

public sealed class ArchitectureTests
{
    private static readonly Assembly Sut = PatternInspector.SystemUnderTest;

    [Fact]
    public void Calls_are_guarded_by_a_circuit_breaker_state()
    {
        Assert.True(
            PatternInspector.HasCircuitBreaker(Sut),
            "Les appels devraient être protégés par un disjoncteur : un état de " +
            "circuit conservé par le client, qui permet d'échouer vite au lieu " +
            "d'appeler une API défaillante.");
    }
}
