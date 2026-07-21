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

// -----------------------------------------------------------------------------
//  CODE MÉTIER À REFACTORISER
// -----------------------------------------------------------------------------
//  La tournée est couplée au TYPE CONCRET des destinataires (RecordingFan) : sa
//  liste et sa méthode d'abonnement connaissent la classe exacte. Notifier un
//  autre type de canal (e-mail, SMS, appli mobile…) obligerait à modifier la
//  tournée. Le sujet et ses observateurs ne sont pas découplés.
//
//  Objectif : que la tournée ne dépende que d'une ABSTRACTION de destinataire,
//  pour notifier n'importe quel abonné sans être modifiée.
// -----------------------------------------------------------------------------
public sealed class ConcertTour
{
    private readonly List<RecordingFan> _fans = new();

    public void Subscribe(RecordingFan fan) => _fans.Add(fan);

    public void AnnounceDate(string city)
    {
        foreach (var fan in _fans)
        {
            fan.Receive($"New tour date in {city}!");
        }
    }
}
