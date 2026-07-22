using System.Reflection;

using Level24.Checkout.Tests.Reflection;

using Xunit;

namespace Level24.Checkout.Tests;

public sealed class ArchitectureTests
{
    private static readonly Assembly Sut = PatternInspector.SystemUnderTest;

    [Fact]
    public void The_transaction_is_a_saga_of_compensable_steps()
    {
        Assert.True(
            PatternInspector.HasSagaOrchestration(Sut),
            "La transaction devrait être orchestrée comme une suite d'étapes " +
            "autonomes (sachant s'exécuter et se compenser), tenue dans une " +
            "collection — au lieu d'un bloc try/catch avec compensation manuelle.");
    }
}
