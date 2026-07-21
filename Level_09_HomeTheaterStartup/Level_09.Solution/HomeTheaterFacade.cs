namespace Level09.HomeTheater;

/// <summary>
/// Façade : regroupe les sous-systèmes et expose UNE opération simple. Le client
/// ne connaît plus ni les composants ni leur ordre d'allumage.
/// </summary>
public sealed class HomeTheaterFacade
{
    private readonly PopcornMaker _popcornMaker = new();
    private readonly Lights _lights = new();
    private readonly Screen _screen = new();
    private readonly Projector _projector = new();
    private readonly Amplifier _amplifier = new();
    private readonly DvdPlayer _dvdPlayer = new();

    public IReadOnlyList<string> WatchMovie()
    {
        return new List<string>
        {
            _popcornMaker.PowerOn(),
            _lights.PowerOn(),
            _screen.PowerOn(),
            _projector.PowerOn(),
            _amplifier.PowerOn(),
            _dvdPlayer.PowerOn(),
        };
    }
}
