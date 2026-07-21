using Xunit;

namespace Level07.Equipment.Tests;

public sealed class BehaviorTests
{
    [Fact]
    public void A_basic_weapon_keeps_its_base_attack()
    {
        Assert.Equal(20, Gear.Basic("Sword", 20).AttackPower());
    }

    [Fact]
    public void Sharpening_adds_attack()
    {
        Assert.Equal(30, Gear.Sharpen(Gear.Basic("Sword", 20)).AttackPower());
    }

    [Fact]
    public void Different_modifiers_stack_together()
    {
        // 20 + 10 (sharpen) + 15 (enchant) = 45
        Assert.Equal(45, Gear.Enchant(Gear.Sharpen(Gear.Basic("Sword", 20))).AttackPower());
    }

    [Fact]
    public void The_same_modifier_can_be_stacked_twice()
    {
        // 20 + 10 + 10 = 40
        Assert.Equal(40, Gear.Sharpen(Gear.Sharpen(Gear.Basic("Sword", 20))).AttackPower());
    }
}
