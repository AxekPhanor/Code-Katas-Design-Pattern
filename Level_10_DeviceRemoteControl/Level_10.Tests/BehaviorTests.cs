using Xunit;

namespace Level10.Devices.Tests;

public sealed class BehaviorTests
{
    [Fact]
    public void The_remote_toggles_power_on_a_television()
    {
        var remote = new RemoteControl(new Television());

        Assert.False(remote.IsOn);
        remote.TogglePower();
        Assert.True(remote.IsOn);
        remote.TogglePower();
        Assert.False(remote.IsOn);
    }

    [Fact]
    public void The_same_remote_controls_a_radio_too()
    {
        var remote = new RemoteControl(new Radio());

        remote.TogglePower();
        remote.SetVolume(30);

        Assert.True(remote.IsOn);
        Assert.Equal(30, remote.Volume);
    }
}
