namespace Level10.Devices;

/// <summary>Marqueur d'appareil : aujourd'hui il n'expose aucune opération commune.</summary>
public interface IDevice
{
    string Name { get; }
    void PowerToggle();
    bool IsOn { get; }
    int Volume { get; set; }
}

public sealed class Television : IDevice
{
    private int _volume;

    public string Name => "TV";
    public bool IsOn { get; private set; }

    public int Volume
    {
        get => _volume;
        set => _volume = Math.Clamp(value, 0, 100);
    }

    public void PowerToggle() => IsOn = !IsOn;
}

public sealed class Radio : IDevice
{
    private int _volume;

    public string Name => "Radio";
    public bool IsOn { get; private set; }

    public int Volume
    {
        get => _volume;
        set => _volume = Math.Clamp(value, 0, 100);
    }

    public void PowerToggle() => IsOn = !IsOn;
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

    public void TogglePower() => _device.PowerToggle();

    public bool IsOn => _device.IsOn;

    public void SetVolume(int level) => _device.Volume = level;

    public int Volume => _device.Volume;
}
