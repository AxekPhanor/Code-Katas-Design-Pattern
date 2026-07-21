namespace Level14.Touring;

/// <summary>Contrat stable d'un destinataire d'alertes.</summary>
public interface ISubscriber
{
    void Receive(string message);
}

/// <summary>Un fan qui enregistre les messages reçus (pour les tests).</summary>
public sealed class RecordingFan : ISubscriber
{
    private readonly List<string> _messages = new();

    public IReadOnlyList<string> Messages => _messages;

    public void Receive(string message) => _messages.Add(message);
}

/// <summary>
/// La tournée ne connaît que l'abstraction <see cref="ISubscriber"/> : n'importe
/// quel abonné peut s'inscrire et sera notifié, sans modifier cette classe.
/// </summary>
public sealed class ConcertTour
{
    private readonly List<ISubscriber> _subscribers = new();

    public void Subscribe(ISubscriber subscriber) => _subscribers.Add(subscriber);

    public void Unsubscribe(ISubscriber subscriber) => _subscribers.Remove(subscriber);

    public void AnnounceDate(string city)
    {
        foreach (var subscriber in _subscribers)
        {
            subscriber.Receive($"New tour date in {city}!");
        }
    }
}
