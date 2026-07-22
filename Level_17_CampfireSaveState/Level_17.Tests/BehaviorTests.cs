using Xunit;

namespace Level17.Campfire.Tests;

public sealed class BehaviorTests
{
    [Fact]
    public void Restoring_reverts_to_the_saved_state()
    {
        var player = new Player();
        player.TakeDamage(30);
        player.Move(5);

        var save = player.Save();

        player.TakeDamage(50);
        player.Move(10);

        player.Restore(save);

        Assert.Equal(70, player.Health);
        Assert.Equal(5, player.Position);
    }

    [Fact]
    public void A_save_captures_an_independent_snapshot()
    {
        var player = new Player();
        player.Move(3);
        var save = player.Save();

        player.Move(4);
        player.Restore(save);

        Assert.Equal(3, player.Position);
    }
}
