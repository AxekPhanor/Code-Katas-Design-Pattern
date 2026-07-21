namespace Level06.Organization;

/// <summary>
/// Abstraction commune : un individu comme une équipe sont des unités
/// organisationnelles, capables de se compter et de se chiffrer elles-mêmes.
/// </summary>
public interface IOrgUnit
{
    string Name { get; }
    int HeadCount();
    decimal MonthlyCost();
}

/// <summary>Feuille : un employé individuel.</summary>
public sealed class Employee : IOrgUnit
{
    public Employee(string name, decimal salary)
    {
        Name = name;
        Salary = salary;
    }

    public string Name { get; }
    public decimal Salary { get; }

    public int HeadCount() => 1;
    public decimal MonthlyCost() => Salary;
}

/// <summary>Composite : une équipe qui contient d'autres unités, uniformément.</summary>
public sealed class Team : IOrgUnit
{
    private readonly List<IOrgUnit> _members = new();

    public Team(string name, decimal managerSalary)
    {
        Name = name;
        ManagerSalary = managerSalary;
    }

    public string Name { get; }
    public decimal ManagerSalary { get; }
    public IReadOnlyList<IOrgUnit> Members => _members;

    public void Add(object member)
    {
        if (member is not IOrgUnit unit)
        {
            throw new ArgumentException("A member must be an org unit.", nameof(member));
        }

        _members.Add(unit);
    }

    public int HeadCount() => 1 + _members.Sum(member => member.HeadCount());

    public decimal MonthlyCost() => ManagerSalary + _members.Sum(member => member.MonthlyCost());
}
