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

/// <summary>État du disjoncteur.</summary>
public enum CircuitState
{
    Closed,
    Open,
    HalfOpen,
}

public sealed class CircuitOpenException : Exception
{
    public CircuitOpenException() : base("Circuit is open; failing fast.")
    {
    }
}

/// <summary>
/// Client protégé par un disjoncteur : après un seuil d'échecs consécutifs, le
/// circuit s'ouvre et les appels échouent VITE, sans toucher à l'API.
/// </summary>
public sealed class ApiClient
{
    private readonly FlakyApi _api;
    private readonly int _failureThreshold = 3;
    private int _consecutiveFailures;
    private CircuitState _state = CircuitState.Closed;

    public ApiClient(FlakyApi api) => _api = api;

    public string Fetch()
    {
        if (_state == CircuitState.Open)
        {
            throw new CircuitOpenException();
        }

        try
        {
            var result = _api.Call();
            _consecutiveFailures = 0;
            _state = CircuitState.Closed;
            return result;
        }
        catch (InvalidOperationException)
        {
            _consecutiveFailures++;
            if (_consecutiveFailures >= _failureThreshold)
            {
                _state = CircuitState.Open;
            }

            throw;
        }
    }
}
