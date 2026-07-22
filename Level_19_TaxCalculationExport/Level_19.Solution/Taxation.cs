namespace Level19.Taxation;

/// <summary>Une opération sur la hiérarchie d'éléments imposables.</summary>
public interface ITaxVisitor
{
    void VisitSalary(Salary salary);
    void VisitDividend(Dividend dividend);
}

/// <summary>Contrat stable d'un élément imposable : il accepte une opération.</summary>
public interface ITaxItem
{
    decimal Amount { get; }
    void Accept(ITaxVisitor visitor);
}

public sealed class Salary : ITaxItem
{
    public Salary(decimal amount) => Amount = amount;
    public decimal Amount { get; }
    public void Accept(ITaxVisitor visitor) => visitor.VisitSalary(this);
}

public sealed class Dividend : ITaxItem
{
    public Dividend(decimal amount) => Amount = amount;
    public decimal Amount { get; }
    public void Accept(ITaxVisitor visitor) => visitor.VisitDividend(this);
}

/// <summary>
/// Une opération = un visiteur. Ajouter "export" ou "audit" = ajouter un
/// visiteur, sans toucher aux éléments de la hiérarchie.
/// </summary>
public sealed class TaxCalculatorVisitor : ITaxVisitor
{
    public decimal Total { get; private set; }

    public void VisitSalary(Salary salary) => Total += salary.Amount * 0.30m;

    public void VisitDividend(Dividend dividend) => Total += dividend.Amount * 0.15m;
}

public static class TaxReport
{
    public static decimal TotalTax(IEnumerable<ITaxItem> items)
    {
        var visitor = new TaxCalculatorVisitor();
        foreach (var item in items)
        {
            item.Accept(visitor);
        }

        return visitor.Total;
    }
}
