namespace Level07.Equipment;

/// <summary>Contrat stable d'un équipement.</summary>
public interface IEquipment
{
    string Name { get; }
    int AttackPower();
}

// -----------------------------------------------------------------------------
//  CODE MÉTIER À REFACTORISER
// -----------------------------------------------------------------------------
//  Chaque modificateur est un simple booléen porté par une unique classe.
//  Conséquences :
//   * on ne peut pas empiler deux fois le même effet (un booléen ne compte pas) ;
//   * chaque nouveau modificateur oblige à ajouter un champ ET à modifier
//     `AttackPower` (explosion combinatoire des combinaisons possibles).
//
//  Objectif : pouvoir empiler dynamiquement des modificateurs, en quantité et
//  dans n'importe quel ordre, sans toucher à la classe de base.
// -----------------------------------------------------------------------------
public sealed class Weapon : IEquipment
{
    private readonly int _baseAttack;

    public Weapon(string name, int baseAttack)
    {
        Name = name;
        _baseAttack = baseAttack;
    }

    public string Name { get; }

    public int AttackPower() => _baseAttack ;
}

public static class Gear
{
    public static IEquipment Basic(string name, int baseAttack) => new Weapon(name, baseAttack);

    public static IEquipment Sharpen(IEquipment equipment) => new Sharpen(equipment);

    public static IEquipment Enchant(IEquipment equipment) => new Enchant(equipment);
}

public class Sharpen : IEquipment
{
    private readonly IEquipment _inner;

    public Sharpen(IEquipment inner)
    {
        _inner = inner;
    }

    public string Name => _inner.Name;

    public int AttackPower() => _inner.AttackPower() + 10;
}

public class Enchant : IEquipment
{
    private readonly IEquipment _inner;

    public Enchant(IEquipment inner)
    {
        _inner = inner;
    }

    public string Name => _inner.Name;

    public int AttackPower() => _inner.AttackPower() + 15;
}