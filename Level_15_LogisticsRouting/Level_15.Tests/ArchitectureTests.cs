using System.Reflection;

using Level15.Logistics.Tests.Reflection;

using Xunit;

namespace Level15.Logistics.Tests;

public sealed class ArchitectureTests
{
    private static readonly Assembly Sut = PatternInspector.SystemUnderTest;

    [Fact]
    public void Routing_algorithms_are_interchangeable_strategies()
    {
        Assert.True(
            PatternInspector.HasStrategy(Sut),
            "Chaque algorithme d'itinéraire devrait être une stratégie autonome " +
            "(implémentant une interface commune), et le planificateur devrait " +
            "déléguer à la stratégie choisie au lieu d'un switch codé en dur.");
    }
}
