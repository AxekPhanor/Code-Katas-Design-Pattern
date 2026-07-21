using System.Reflection;

using Level02.Bestiary.Tests.Reflection;

using Xunit;

namespace Level02.Bestiary.Tests;

/// <summary>
/// Vérifie que le bon patron a été appliqué, sans imposer de noms de classes.
/// </summary>
public sealed class ArchitectureTests
{
    private static readonly Assembly Sut = PatternInspector.SystemUnderTest;

    [Fact]
    public void An_enemy_can_produce_a_copy_of_itself()
    {
        Assert.True(
            PatternInspector.HasSelfCloningProduct(Sut, typeof(IEnemy)),
            "Un ennemi devrait pouvoir se dupliquer lui-même (une méthode sans " +
            "paramètre renvoyant un nouvel IEnemy), afin que le spawner n'ait " +
            "plus à le reconstruire champ par champ.");
    }
}
