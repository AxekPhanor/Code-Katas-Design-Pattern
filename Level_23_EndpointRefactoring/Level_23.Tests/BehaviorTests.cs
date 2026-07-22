using Xunit;

namespace Level23.Endpoints.Tests;

public sealed class BehaviorTests
{
    [Fact]
    public void A_valid_registration_succeeds()
    {
        var response = Api.RegisterUser("alice@example.com", "password1");

        Assert.True(response.Success);
        Assert.Equal("user-alice@example.com", response.UserId);
    }

    [Fact]
    public void An_invalid_registration_fails()
    {
        var response = Api.RegisterUser("not-an-email", "short");

        Assert.False(response.Success);
    }
}
