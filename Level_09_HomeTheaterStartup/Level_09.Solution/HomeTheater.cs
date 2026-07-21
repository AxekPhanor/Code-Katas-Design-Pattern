namespace Level09.HomeTheater;

/// <summary>Point d'entrée : délègue toute l'orchestration à la façade.</summary>
public static class HomeTheater
{
    public static IReadOnlyList<string> StartMovie() => new HomeTheaterFacade().WatchMovie();
}
