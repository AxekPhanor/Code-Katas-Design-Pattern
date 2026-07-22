using System.Reflection;

using Level20.Configuration.Tests.Reflection;

using Xunit;

namespace Level20.Configuration.Tests;

public sealed class ArchitectureTests
{
    private static readonly Assembly Sut = PatternInspector.SystemUnderTest;

    [Fact]
    public void Configuration_is_bound_to_a_strongly_typed_options_object()
    {
        Assert.True(
            PatternInspector.HasStronglyTypedOptions(Sut),
            "La configuration devrait être liée à un objet fortement typé (une " +
            "classe d'options du projet, détenue par le service), au lieu de lire " +
            "des clés de chaîne éparpillées.");
    }
}
