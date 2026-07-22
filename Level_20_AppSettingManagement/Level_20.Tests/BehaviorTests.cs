using Xunit;

namespace Level20.Configuration.Tests;

public sealed class BehaviorTests
{
    private static Dictionary<string, string> SampleConfig(bool useSsl) => new()
    {
        ["Smtp:Host"] = "mail.example.com",
        ["Smtp:Port"] = "587",
        ["Smtp:UseSsl"] = useSsl ? "true" : "false",
    };

    [Fact]
    public void The_service_describes_the_configured_endpoint()
    {
        var service = new EmailService(SampleConfig(useSsl: true));

        Assert.Equal("mail.example.com:587 (SSL)", service.Describe());
    }

    [Fact]
    public void Ssl_is_reflected_in_the_description()
    {
        var service = new EmailService(SampleConfig(useSsl: false));

        Assert.Equal("mail.example.com:587", service.Describe());
    }
}
