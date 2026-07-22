namespace Level16.Combat;

// -----------------------------------------------------------------------------
//  CODE MÉTIER À REFACTORISER
// -----------------------------------------------------------------------------
//  L'état de combat est une simple chaîne, et CHAQUE action teste cet état pour
//  décider du comportement et de la transition. La logique d'un même état est
//  éparpillée dans toutes les méthodes ; ajouter un état (ex. « enragé ») force à
//  toucher à toutes les actions et multiplie les conditions.
//
//  Objectif : faire de chaque état un objet autonome qui décide lui-même de ses
//  transitions, et laisser le personnage déléguer à l'état courant.
// -----------------------------------------------------------------------------
public sealed class Character
{
    public string State { get; private set; } = "Idle";

    public void Attack()
    {
        if (State != "Stunned")
        {
            State = "Attacking";
        }
    }

    public void Defend()
    {
        if (State != "Stunned")
        {
            State = "Defending";
        }
    }

    public void Recover()
    {
        State = "Idle";
    }

    public void TakeHit()
    {
        if (State != "Defending")
        {
            State = "Stunned";
        }
    }
}
