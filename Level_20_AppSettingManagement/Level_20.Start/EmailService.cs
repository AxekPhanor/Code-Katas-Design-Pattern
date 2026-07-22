namespace Level20.Configuration;

// -----------------------------------------------------------------------------
//  CODE MÉTIER À REFACTORISER
// -----------------------------------------------------------------------------
//  La configuration est lue « à la ficelle » : des clés magiques (`"Smtp:Port"`)
//  et des conversions (`int.Parse`, `bool.Parse`) éparpillées, réévaluées à
//  chaque usage. Aucune validation centrale, aucune complétion, aucune sûreté de
//  type : une clé mal orthographiée explose à l'exécution.
//
//  Objectif : lier une bonne fois la configuration à un objet FORTEMENT TYPÉ, et
//  ne manipuler ensuite que ses propriétés.
// -----------------------------------------------------------------------------
public sealed class EmailService
{
    private readonly IReadOnlyDictionary<string, string> _config;

    public EmailService(IReadOnlyDictionary<string, string> config) => _config = config;

    public string Describe()
    {
        var host = _config["Smtp:Host"];
        var port = int.Parse(_config["Smtp:Port"]);
        var useSsl = bool.Parse(_config["Smtp:UseSsl"]);

        return $"{host}:{port}{(useSsl ? " (SSL)" : string.Empty)}";
    }
}
