using Xunit;

namespace Level02.Bestiary.Tests;

/// <summary>
/// Vérifie que le spawn produit des ennemis corrects et indépendants.
/// </summary>
public sealed class BehaviorTests
{
    private static Enemy Prototype() => new("Orc", 100, 15, new[] { "Smash", "Roar" });

    [Fact]
    public void Spawned_enemy_matches_the_prototype_stats()
    {
        var enemy = new EnemySpawner(Prototype()).Spawn();

        Assert.Equal("Orc", enemy.Name);
        Assert.Equal(100, enemy.Health);
        Assert.Equal(15, enemy.Damage);
        Assert.Equal(new[] { "Smash", "Roar" }, enemy.Abilities);
    }

    [Fact]
    public void Each_spawn_is_an_independent_instance()
    {
        var spawner = new EnemySpawner(Prototype());

        var first = spawner.Spawn();
        var second = spawner.Spawn();

        Assert.NotSame(first, second);

        first.TakeDamage(40);

        Assert.Equal(60, first.Health);
        Assert.Equal(100, second.Health);
    }

    [Fact]
    public void Damaging_a_spawn_does_not_affect_the_prototype()
    {
        var prototype = Prototype();
        var spawned = new EnemySpawner(prototype).Spawn();

        spawned.TakeDamage(50);

        Assert.Equal(100, prototype.Health);
    }
}
