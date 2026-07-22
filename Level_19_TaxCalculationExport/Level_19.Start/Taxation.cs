namespace Level19.Taxation;

/// <summary>Contrat stable d'un élément imposable.</summary>
public interface ITaxItem
{
    decimal Amount { get; }
}

public sealed class Salary : ITaxItem
{
    public Salary(decimal amount) => Amount = amount;
    public decimal Amount { get; }
}

public sealed class Dividend : ITaxItem
{
    public Dividend(decimal amount) => Amount = amount;
    public decimal Amount { get; }
}

// -----------------------------------------------------------------------------
//  CODE MÉTIER À REFACTORISER
// -----------------------------------------------------------------------------
//  Chaque opération sur la hiérarchie (ici le calcul d'impôt) est un `switch` de
//  type (`is Salary` / `is Dividend`). Ajouter une OPÉRATION (export, audit…)
//  duplique ce switch ; ajouter un TYPE d'élément oblige à rouvrir toutes les
//  opérations existantes.
//
//  Objectif : pouvoir ajouter une nouvelle opération sur la hiérarchie sans la
//  modifier, en faisant "visiter" chaque élément par l'opération.
// -----------------------------------------------------------------------------
public static class TaxReport
{
    public static decimal TotalTax(IEnumerable<ITaxItem> items)
    {
        decimal total = 0m;
        foreach (var item in items)
        {
            if (item is Salary salary)
            {
                total += salary.Amount * 0.30m;
            }
            else if (item is Dividend dividend)
            {
                total += dividend.Amount * 0.15m;
            }
            else
            {
                throw new NotSupportedException($"Unknown tax item: {item.GetType().Name}");
            }
        }

        return total;
    }
}
