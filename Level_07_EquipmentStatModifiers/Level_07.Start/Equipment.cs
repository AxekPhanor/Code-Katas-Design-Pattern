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
    public bool Sharpened { get; set; }
    public bool Enchanted { get; set; }

    public int AttackPower() =>
        _baseAttack + (Sharpened ? 10 : 0) + (Enchanted ? 15 : 0);
}

public static class Gear
{
    public static IEquipment Basic(string name, int baseAttack) => new Weapon(name, baseAttack);

    public static IEquipment Sharpen(IEquipment equipment)
    {
        ((Weapon)equipment).Sharpened = true;
        return equipment;
    }

    public static IEquipment Enchant(IEquipment equipment)
    {
        ((Weapon)equipment).Enchanted = true;
        return equipment;
    }
}
