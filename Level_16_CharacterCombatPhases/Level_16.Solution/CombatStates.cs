namespace Level16.Combat;

/// <summary>
/// Un état de combat autonome : il porte son nom et décide lui-même de la
/// transition provoquée par chaque action.
/// </summary>
public interface ICombatState
{
    string Name { get; }
    ICombatState Attack();
    ICombatState Defend();
    ICombatState Recover();
    ICombatState TakeHit();
}

public sealed class IdleState : ICombatState
{
    public string Name => "Idle";
    public ICombatState Attack() => new AttackingState();
    public ICombatState Defend() => new DefendingState();
    public ICombatState Recover() => this;
    public ICombatState TakeHit() => new StunnedState();
}

public sealed class AttackingState : ICombatState
{
    public string Name => "Attacking";
    public ICombatState Attack() => this;
    public ICombatState Defend() => new DefendingState();
    public ICombatState Recover() => new IdleState();
    public ICombatState TakeHit() => new StunnedState();
}

public sealed class DefendingState : ICombatState
{
    public string Name => "Defending";
    public ICombatState Attack() => new AttackingState();
    public ICombatState Defend() => this;
    public ICombatState Recover() => new IdleState();
    public ICombatState TakeHit() => this; // la garde bloque le coup
}

public sealed class StunnedState : ICombatState
{
    public string Name => "Stunned";
    public ICombatState Attack() => this; // sonné : aucune action possible
    public ICombatState Defend() => this;
    public ICombatState Recover() => new IdleState();
    public ICombatState TakeHit() => this;
}
