using System.Reflection;

using Level01.AssemblyPlant.Tests.Reflection;

using Xunit;

namespace Level01.AssemblyPlant.Tests;

/// <summary>
/// Vérifie que le bon patron a été appliqué. L'analyse est agnostique aux noms :
/// elle observe la forme du code, pas la façon dont tu nommes tes classes.
/// </summary>
public sealed class ArchitectureTests
{
    private static readonly Assembly Sut = PatternInspector.SystemUnderTest;

    [Fact]
    public void The_factory_is_not_built_through_a_giant_constructor()
    {
        var offenders = PatternInspector.TelescopingConstructors(
            Sut, typeof(IAssemblyFactory), maxParameters: 3);

        Assert.True(
            offenders.Count == 0,
            "La ligne de production devrait s'assembler étape par étape. " +
            "Constructeur(s) public(s) trop volumineux détecté(s) : " +
            string.Join(", ", offenders.Select(
                c => $"{c.DeclaringType!.Name}({c.GetParameters().Length} paramètres)")));
    }

    [Fact]
    public void A_dedicated_assembler_builds_the_line_step_by_step()
    {
        Assert.True(
            PatternInspector.HasStepwiseAssembler(Sut, typeof(IAssemblyFactory)),
            "Un type dédié devrait assembler une IAssemblyFactory par étapes " +
            "chaînables (au moins deux méthodes qui se renvoient elles-mêmes, " +
            "plus une méthode de finalisation qui renvoie l'usine).");
    }
}
