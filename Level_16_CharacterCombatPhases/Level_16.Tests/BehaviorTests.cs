using Xunit;

namespace Level16.Combat.Tests;

public sealed class BehaviorTests
{
    [Fact]
    public void A_character_starts_idle()
    {
        Assert.Equal("Idle", new Character().State);
    }

    [Fact]
    public void Attacking_moves_to_the_attacking_state()
    {
        var character = new Character();
        character.Attack();
        Assert.Equal("Attacking", character.State);
    }

    [Fact]
    public void Taking_a_hit_while_idle_stuns_the_character()
    {
        var character = new Character();
        character.TakeHit();
        Assert.Equal("Stunned", character.State);
    }

    [Fact]
    public void Defending_blocks_a_hit_and_stays_defending()
    {
        var character = new Character();
        character.Defend();
        character.TakeHit();
        Assert.Equal("Defending", character.State);
    }

    [Fact]
    public void A_stunned_character_cannot_act_until_it_recovers()
    {
        var character = new Character();
        character.TakeHit();

        character.Attack();
        Assert.Equal("Stunned", character.State);

        character.Recover();
        Assert.Equal("Idle", character.State);
    }
}
