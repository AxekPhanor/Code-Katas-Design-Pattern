using System.Reflection;

using Level16.Combat.Tests.Reflection;

using Xunit;

namespace Level16.Combat.Tests;

public sealed class ArchitectureTests
{
    private static readonly Assembly Sut = PatternInspector.SystemUnderTest;

    [Fact]
    public void Each_combat_phase_is_a_state_that_owns_its_transitions()
    {
        Assert.True(
            PatternInspector.HasStateMachine(Sut),
            "Chaque phase de combat devrait être un état objet dont les actions " +
            "renvoient l'état suivant, et le personnage devrait déléguer à l'état " +
            "courant — au lieu de tester une chaîne d'état dans chaque méthode.");
    }
}
