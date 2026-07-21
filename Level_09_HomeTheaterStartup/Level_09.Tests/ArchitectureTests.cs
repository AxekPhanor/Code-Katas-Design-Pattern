using System.Reflection;

using Level09.HomeTheater.Tests.Reflection;

using Xunit;

namespace Level09.HomeTheater.Tests;

public sealed class ArchitectureTests
{
    private static readonly Assembly Sut = PatternInspector.SystemUnderTest;

    [Fact]
    public void A_single_facade_aggregates_and_orchestrates_the_subsystems()
    {
        Assert.True(
            PatternInspector.HasFacade(Sut, typeof(ISubsystem)),
            "Une façade dédiée devrait regrouper les sous-systèmes (au moins trois, " +
            "en champs) et exposer une opération simple, plutôt que de laisser " +
            "l'orchestration éparpillée dans une procédure.");
    }
}
