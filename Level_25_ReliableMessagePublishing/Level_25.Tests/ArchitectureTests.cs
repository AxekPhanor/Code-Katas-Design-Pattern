using System.Reflection;

using Level25.Messaging.Tests.Reflection;

using Xunit;

namespace Level25.Messaging.Tests;

public sealed class ArchitectureTests
{
    private static readonly Assembly Sut = PatternInspector.SystemUnderTest;

    [Fact]
    public void Events_are_persisted_in_an_outbox_before_being_published()
    {
        Assert.True(
            PatternInspector.HasOutbox(Sut),
            "L'événement devrait être persisté dans une boîte d'envoi (une " +
            "collection de messages dédiés), puis publié par un relais — au lieu " +
            "d'être publié directement et perdu en cas de panne.");
    }
}
