using Xunit;

namespace Level26.Resilience.Tests;

public sealed class BehaviorTests
{
    [Fact]
    public void A_successful_call_passes_through()
    {
        var api = new FlakyApi { ShouldFail = false };
        var client = new ApiClient(api);

        Assert.Equal("OK", client.Fetch());
    }

    [Fact]
    public void After_repeated_failures_the_circuit_opens_and_stops_calling_the_api()
    {
        var api = new FlakyApi { ShouldFail = true };
        var client = new ApiClient(api);

        for (var i = 0; i < 6; i++)
        {
            try
            {
                client.Fetch();
            }
            catch
            {
                // On ignore l'échec : on observe seulement le nombre d'appels réels.
            }
        }

        // Sans protection, l'API est appelée 6 fois. Avec un disjoncteur, elle
        // cesse d'être appelée une fois le circuit ouvert.
        Assert.True(api.CallCount <= 3);
    }
}
