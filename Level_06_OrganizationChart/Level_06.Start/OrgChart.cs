namespace Level06.Organization;

// -----------------------------------------------------------------------------
//  CODE MÉTIER À REFACTORISER
// -----------------------------------------------------------------------------
//  Pour parcourir la hiérarchie, ce code doit distinguer À LA MAIN chaque type
//  de membre (`is Employee` / `is Team`). Chaque nouveau type d'unité
//  organisationnelle oblige à rouvrir CHACUNE de ces méthodes et à ajouter un
//  test de type. Individus et équipes ne sont pas traités de façon uniforme.
//
//  Objectif : traiter feuilles et groupes à travers une même abstraction, pour
//  supprimer ces tests de type et laisser chaque unité se calculer elle-même.
// -----------------------------------------------------------------------------
public static class OrgChart
{
    public static int HeadCount(Team team)
    {
        var count = 1; // le manager
        foreach (var member in team.Members)
        {
            if (member is Employee)
            {
                count += 1;
            }
            else if (member is Team subTeam)
            {
                count += HeadCount(subTeam);
            }
            else
            {
                throw new NotSupportedException($"Unknown org unit: {member.GetType().Name}");
            }
        }

        return count;
    }

    public static decimal MonthlyCost(Team team)
    {
        var cost = team.ManagerSalary;
        foreach (var member in team.Members)
        {
            if (member is Employee employee)
            {
                cost += employee.Salary;
            }
            else if (member is Team subTeam)
            {
                cost += MonthlyCost(subTeam);
            }
            else
            {
                throw new NotSupportedException($"Unknown org unit: {member.GetType().Name}");
            }
        }

        return cost;
    }
}
