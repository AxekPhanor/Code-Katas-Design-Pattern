namespace Level06.Organization;

/// <summary>
/// Plus aucun test de type : chaque unité sait se compter et se chiffrer.
/// Le point d'entrée délègue simplement à la racine.
/// </summary>
public static class OrgChart
{
    public static int HeadCount(Team team) => team.HeadCount();

    public static decimal MonthlyCost(Team team) => team.MonthlyCost();
}
