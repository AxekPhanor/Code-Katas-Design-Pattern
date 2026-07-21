using System.Reflection;

using Level13.Editing.Tests.Reflection;

using Xunit;

namespace Level13.Editing.Tests;

public sealed class ArchitectureTests
{
    private static readonly Assembly Sut = PatternInspector.SystemUnderTest;

    [Fact]
    public void Actions_are_reified_as_commands_kept_in_a_history()
    {
        Assert.True(
            PatternInspector.HasCommandHistory(Sut),
            "Chaque action devrait être encapsulée dans une commande (sachant " +
            "s'exécuter et s'annuler), et l'éditeur devrait conserver ces commandes " +
            "dans un historique, au lieu de photographier l'état complet.");
    }
}
