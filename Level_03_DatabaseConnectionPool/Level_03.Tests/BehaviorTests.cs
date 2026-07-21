using System.Collections.Concurrent;

using Xunit;

namespace Level03.Persistence.Tests;

/// <summary>
/// Vérifie l'unicité et le partage de la ressource, y compris en concurrence.
/// </summary>
public sealed class BehaviorTests
{
    [Fact]
    public void GetInstance_always_returns_the_same_shared_instance()
    {
        Assert.Same(ConnectionPool.GetInstance(), ConnectionPool.GetInstance());
    }

    [Fact]
    public void The_pool_is_created_only_once()
    {
        _ = ConnectionPool.GetInstance();
        _ = ConnectionPool.GetInstance();

        Assert.Equal(1, ConnectionPool.ConstructionCount);
    }

    [Fact]
    public void Concurrent_access_yields_a_single_instance()
    {
        var seen = new ConcurrentBag<ConnectionPool>();

        Parallel.For(0, 100, _ => seen.Add(ConnectionPool.GetInstance()));

        Assert.Single(seen.Distinct());
    }
}
