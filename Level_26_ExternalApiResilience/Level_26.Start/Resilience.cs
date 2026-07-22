namespace Level26.Resilience;

/// <summary>Une API tierce instable, qui compte ses appels.</summary>
public sealed class FlakyApi
{
    public int CallCount { get; private set; }

    public bool ShouldFail { get; set; }

    public string Call()
    {
        CallCount++;
        if (ShouldFail)
        {
            throw new InvalidOperationException("External API failure.");
        }

        return "OK";
    }
}

// -----------------------------------------------------------------------------
//  CODE MÉTIER À REFACTORISER
// -----------------------------------------------------------------------------
//  Chaque appel part directement vers l'API tierce, sans aucune protection.
//  Quand elle tombe, on continue de la marteler à chaque tentative : les échecs
//  se propagent, les délais s'accumulent, la panne peut se cascader.
//
//  Objectif : après un certain nombre d'échecs consécutifs, "ouvrir le circuit"
//  et échouer VITE sans appeler l'API, le temps qu'elle se rétablisse.
// -----------------------------------------------------------------------------
public sealed class ApiClient
{
    private readonly FlakyApi _api;

    public ApiClient(FlakyApi api) => _api = api;

    public string Fetch() => _api.Call();
}
