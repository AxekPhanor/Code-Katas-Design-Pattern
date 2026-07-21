using System.Reflection;

using Level14.Touring.Tests.Reflection;

using Xunit;

namespace Level14.Touring.Tests;

public sealed class ArchitectureTests
{
    private static readonly Assembly Sut = PatternInspector.SystemUnderTest;

    [Fact]
    public void The_tour_notifies_subscribers_through_an_abstraction()
    {
        Assert.True(
            PatternInspector.HasObserverRegistry(Sut),
            "La tournée devrait conserver ses abonnés via l'ABSTRACTION " +
            "(une collection d'ISubscriber), et non via un type concret, afin de " +
            "notifier n'importe quel abonné sans être modifiée.");
    }
}
