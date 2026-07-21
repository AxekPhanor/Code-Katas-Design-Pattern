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
    public static IReadOnlyList<string> StartMovie()
    {
        return new List<string>
        {
            new PopcornMaker().PowerOn(),
            new Lights().PowerOn(),
            new Screen().PowerOn(),
            new Projector().PowerOn(),
            new Amplifier().PowerOn(),
            new DvdPlayer().PowerOn(),
        };
    }
}
