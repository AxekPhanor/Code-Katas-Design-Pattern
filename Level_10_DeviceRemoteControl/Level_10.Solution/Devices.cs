namespace Level10.Devices;

/// <summary>
/// Interface de commande commune (l'implémenteur). Chaque appareil sait
/// répondre à ces commandes ; la télécommande n'a plus à connaître les types.
/// </summary>
public interface IDevice
{
    string Name { get; }
    bool IsEnabled { get; }
    int Volume { get; }
    void Enable();
    void Disable();
    void SetVolume(int level);
}

public sealed class Television : IDevice
{
    public string Name => "TV";
    public bool IsEnabled { get; private set; }
    public int Volume { get; private set; }

    public void Enable() => IsEnabled = true;
    public void Disable() => IsEnabled = false;
    public void SetVolume(int level) => Volume = Math.Clamp(level, 0, 100);
}

public sealed class Radio : IDevice
{
    public string Name => "Radio";
    public bool IsEnabled { get; private set; }
    public int Volume { get; private set; }

    public void Enable() => IsEnabled = true;
    public void Disable() => IsEnabled = false;
    public void SetVolume(int level) => Volume = Math.Clamp(level, 0, 100);
}

/// <summary>
/// L'abstraction : elle référence un implémenteur (IDevice) et lui délègue,
/// sans aucun test de type. Elle peut être raffinée indépendamment des appareils.
/// </summary>
public class RemoteControl
{
    private readonly IDevice _device;

    public RemoteControl(IDevice device) => _device = device;

    public void TogglePower()
    {
        if (_device.IsEnabled)
        {
            _device.Disable();
        }
        else
        {
            _device.Enable();
        }
    }

    public bool IsOn => _device.IsEnabled;

    public void SetVolume(int level) => _device.SetVolume(level);

    public int Volume => _device.Volume;
}

/// <summary>Une télécommande raffinée : varie indépendamment des appareils.</summary>
public sealed class AdvancedRemoteControl : RemoteControl
{
    public AdvancedRemoteControl(IDevice device) : base(device)
    {
    }

    public void Mute() => SetVolume(0);
}
