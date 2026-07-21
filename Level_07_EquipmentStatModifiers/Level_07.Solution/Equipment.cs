namespace Level07.Equipment;

/// <summary>Contrat stable d'un équipement.</summary>
public interface IEquipment
{
    string Name { get; }
    int AttackPower();
}

/// <summary>L'équipement de base, sans aucun modificateur.</summary>
public sealed class Weapon : IEquipment
{
    private readonly int _baseAttack;

    public Weapon(string name, int baseAttack)
    {
        Name = name;
        _baseAttack = baseAttack;
    }

    public string Name { get; }

    public int AttackPower() => _baseAttack;
}

/// <summary>Modificateur : enveloppe un équipement et lui ajoute de l'attaque.</summary>
public sealed class SharpenedModifier : IEquipment
{
    private readonly IEquipment _inner;

    public SharpenedModifier(IEquipment inner) => _inner = inner;

    public string Name => $"Sharpened {_inner.Name}";

    public int AttackPower() => _inner.AttackPower() + 10;
}

public sealed class EnchantedModifier : IEquipment
{
    private readonly IEquipment _inner;

    public EnchantedModifier(IEquipment inner) => _inner = inner;

    public string Name => $"Enchanted {_inner.Name}";

    public int AttackPower() => _inner.AttackPower() + 15;
}

public static class Gear
{
    public static IEquipment Basic(string name, int baseAttack) => new Weapon(name, baseAttack);

    public static IEquipment Sharpen(IEquipment equipment) => new SharpenedModifier(equipment);

    public static IEquipment Enchant(IEquipment equipment) => new EnchantedModifier(equipment);
}
