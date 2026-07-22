namespace Level16.Combat;

/// <summary>
/// Le personnage ne teste plus aucun état : il délègue chaque action à l'état
/// courant, qui renvoie l'état suivant.
/// </summary>
public sealed class Character
{
    private ICombatState _state = new IdleState();

    public string State => _state.Name;

    public void Attack() => _state = _state.Attack();

    public void Defend() => _state = _state.Defend();

    public void Recover() => _state = _state.Recover();

    public void TakeHit() => _state = _state.TakeHit();
}
