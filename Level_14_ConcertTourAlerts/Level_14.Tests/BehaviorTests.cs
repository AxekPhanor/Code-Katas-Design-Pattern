using Xunit;

namespace Level14.Touring.Tests;

public sealed class BehaviorTests
{
    [Fact]
    public void Announcing_a_date_notifies_every_subscribed_fan()
    {
        var tour = new ConcertTour();
        var alice = new RecordingFan();
        var bob = new RecordingFan();
        tour.Subscribe(alice);
        tour.Subscribe(bob);

        tour.AnnounceDate("Paris");

        Assert.Contains("New tour date in Paris!", alice.Messages);
        Assert.Contains("New tour date in Paris!", bob.Messages);
    }

    [Fact]
    public void A_fan_receives_each_announced_date()
    {
        var tour = new ConcertTour();
        var fan = new RecordingFan();
        tour.Subscribe(fan);

        tour.AnnounceDate("Paris");
        tour.AnnounceDate("London");

        Assert.Equal(2, fan.Messages.Count);
    }
}
