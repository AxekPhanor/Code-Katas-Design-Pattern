using System.Reflection;

using Level10.Devices.Tests.Reflection;

using Xunit;

namespace Level10.Devices.Tests;

public sealed class ArchitectureTests
{
    private static readonly Assembly Sut = PatternInspector.SystemUnderTest;

    [Fact]
    public void The_remote_delegates_to_a_common_device_command_interface()
    {
        Assert.True(
            PatternInspector.HasBridge(Sut, typeof(IDevice)),
            "Les appareils devraient exposer une interface de commandes commune " +
            "(au moins deux opérations), et la télécommande devrait lui déléguer " +
            "au lieu de tester le type concret de chaque appareil.");
    }
}
