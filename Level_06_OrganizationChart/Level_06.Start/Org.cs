namespace Level06.Organization;

/// <summary>Un employé individuel.</summary>
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

/// <summary>
/// Une équipe : un manager, et des membres hétérogènes stockés "en vrac".
/// </summary>
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

    public void Add(IOrgUnit member) => _members.Add(member);

    public int HeadCount() => 1 + _members.Sum(m => m.HeadCount());
    public decimal MonthlyCost() => ManagerSalary + _members.Sum(m => m.MonthlyCost());
}
