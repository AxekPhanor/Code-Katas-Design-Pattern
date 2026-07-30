namespace Level09.HomeTheater;

// -----------------------------------------------------------------------------
//  CODE MÉTIER À REFACTORISER
// -----------------------------------------------------------------------------
//  L'allumage des 6 sous-systèmes vit "en vrac" dans une procédure : le client
//  doit connaître chaque sous-système et l'ordre exact d'allumage. Aucun objet
//  n'encapsule cette séquence, elle serait à recopier partout où l'on veut
//  lancer un film.
//
//  Objectif : offrir un point d'entrée unique et simple qui orchestre les
//  sous-systèmes, sans exposer leur complexité au client.
// -----------------------------------------------------------------------------
public static class HomeTheater
{
    public static IReadOnlyList<string> StartMovie() => new FacadeHomeTheater().Watch();
}

public class HomeTheaterFacade
{
    private readonly PopcornMaker _popcorn;
    private readonly Lights _lights;
    private readonly Screen _screen;
    private readonly Projector _projector;
    private readonly Amplifier _amplifier;
    private readonly DvdPlayer _dvd;

    public HomeTheaterFacade()
    {
        _popcorn   = new PopcornMaker();
        _lights    = new Lights();
        _screen    = new Screen();
        _projector = new Projector();
        _amplifier = new Amplifier();
        _dvd       = new DvdPlayer();
    }

    public IReadOnlyList<string> Watch()
    {
        return new List<string>
        {
            _popcorn.PowerOn(),
            _lights.PowerOn(),
            _screen.PowerOn(),
            _projector.PowerOn(),
            _amplifier.PowerOn(),
            _dvd.PowerOn(),
        };
    }
}