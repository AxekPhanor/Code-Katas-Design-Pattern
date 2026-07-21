namespace Level10.Devices;

/// <summary>Marqueur d'appareil : aujourd'hui il n'expose aucune opération commune.</summary>
public interface IDevice
{
    string Name { get; }
}

public sealed class Television : IDevice
{
    public string Name => "TV";
    public bool Powered { get; private set; }
    public int Level { get; private set; }

    public void Switch() => Powered = !Powered;
    public void SetLevel(int level) => Level = Math.Clamp(level, 0, 100);
}

public sealed class Radio : IDevice
{
    public string Name => "Radio";
    public bool Active { get; private set; }
    public int Loudness { get; private set; }

    public void Toggle() => Active = !Active;
    public void Tune(int level) => Loudness = Math.Clamp(level, 0, 100);
}

// -----------------------------------------------------------------------------
//  CODE MÉTIER À REFACTORISER
// -----------------------------------------------------------------------------
//  La télécommande doit tester À LA MAIN le type concret de l'appareil dans
//  CHAQUE opération, car les appareils n'exposent aucune commande commune. Ajouter
//  un appareil (ampli, projecteur…) oblige à rouvrir toutes ces méthodes ; faire
//  évoluer la télécommande et faire évoluer les appareils sont deux axes soudés.
//
//  Objectif : donner aux appareils une interface de commandes commune, et faire
//  déléguer la télécommande à cette interface — pour que les deux hiérarchies
//  (télécommande / appareil) évoluent indépendamment.
// -----------------------------------------------------------------------------
public sealed class RemoteControl
{
    private readonly IDevice _device;

    public RemoteControl(IDevice device) => _device = device;

    public void TogglePower()
    {
        if (_device is Television tv)
        {
            tv.Switch();
        }
        else if (_device is Radio radio)
        {
            radio.Toggle();
        }
        else
        {
            throw new NotSupportedException($"Unknown device: {_device.Name}");
        }
    }

    public bool IsOn => _device switch
    {
        Television tv => tv.Powered,
        Radio radio => radio.Active,
        _ => throw new NotSupportedException($"Unknown device: {_device.Name}"),
    };

    public void SetVolume(int level)
    {
        if (_device is Television tv)
        {
            tv.SetLevel(level);
        }
        else if (_device is Radio radio)
        {
            radio.Tune(level);
        }
        else
        {
            throw new NotSupportedException($"Unknown device: {_device.Name}");
        }
    }

    public int Volume => _device switch
    {
        Television tv => tv.Level,
        Radio radio => radio.Loudness,
        _ => throw new NotSupportedException($"Unknown device: {_device.Name}"),
    };
}
