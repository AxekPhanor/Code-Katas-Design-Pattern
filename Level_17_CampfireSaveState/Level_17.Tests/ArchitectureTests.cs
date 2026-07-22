using System.Reflection;

using Level17.Campfire.Tests.Reflection;

using Xunit;

namespace Level17.Campfire.Tests;

public sealed class ArchitectureTests
{
    private static readonly Assembly Sut = PatternInspector.SystemUnderTest;

    [Fact]
    public void The_save_is_an_opaque_encapsulated_token()
    {
        Assert.True(
            PatternInspector.HasMemento(Sut, typeof(Player)),
            "La sauvegarde devrait être un jeton opaque (renvoyé par Save, accepté " +
            "par Restore) sans membre public modifiable, afin de ne pas exposer " +
            "l'état interne du joueur.");
    }
}
