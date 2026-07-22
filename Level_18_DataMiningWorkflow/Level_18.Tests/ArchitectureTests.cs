using System.Reflection;

using Level18.DataMining.Tests.Reflection;

using Xunit;

namespace Level18.DataMining.Tests;

public sealed class ArchitectureTests
{
    private static readonly Assembly Sut = PatternInspector.SystemUnderTest;

    [Fact]
    public void The_workflow_skeleton_is_fixed_with_redefinable_steps()
    {
        Assert.True(
            PatternInspector.HasTemplateMethod(Sut),
            "Le squelette de l'algorithme devrait être figé une seule fois dans une " +
            "classe abstraite (méthode concrète), avec des étapes abstraites " +
            "redéfinies par au moins deux sous-classes — au lieu d'un switch dupliquant " +
            "le déroulé.");
    }
}
