using System.Reflection;

using Level21.Composition.Tests.Reflection;

using Xunit;

namespace Level21.Composition.Tests;

public sealed class ArchitectureTests
{
    private static readonly Assembly Sut = PatternInspector.SystemUnderTest;

    [Fact]
    public void Registration_is_split_into_cohesive_module_extensions()
    {
        Assert.True(
            PatternInspector.HasModularRegistrationExtensions(Sut, typeof(ServiceRegistry)),
            "L'enregistrement devrait être découpé en plusieurs méthodes d'extension " +
            "sur le registre (une par module cohérent), au lieu d'une seule méthode " +
            "obèse.");
    }
}
