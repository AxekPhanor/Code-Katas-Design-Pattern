using Xunit;

namespace Level11.AccessControl.Tests;

public sealed class BehaviorTests
{
    [Fact]
    public void Reading_a_user_returns_the_name()
    {
        var repository = UserAccess.Wrap(new DatabaseUserRepository());

        Assert.Equal("User#1", repository.GetUserName(1));
    }

    [Fact]
    public void Repeated_reads_of_the_same_user_hit_the_database_only_once()
    {
        var database = new DatabaseUserRepository();
        var repository = UserAccess.Wrap(database);

        repository.GetUserName(1);
        repository.GetUserName(1);
        repository.GetUserName(1);

        Assert.Equal(1, database.QueryCount);
    }
}
