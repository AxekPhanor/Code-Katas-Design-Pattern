using Xunit;

namespace Level09.HomeTheater.Tests;

public sealed class BehaviorTests
{
    [Fact]
    public void Starting_a_movie_powers_on_all_six_subsystems_in_order()
    {
        var log = HomeTheater.StartMovie();

        Assert.Equal(
            new[]
            {
                "PopcornMaker on",
                "Lights on",
                "Screen on",
                "Projector on",
                "Amplifier on",
                "DvdPlayer on",
            },
            log);
    }
}
