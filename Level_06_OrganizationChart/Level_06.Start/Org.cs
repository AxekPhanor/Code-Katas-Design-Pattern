namespace Level06.Organization;

/// <summary>Un employé individuel.</summary>
public sealed class Employee
{
    public Employee(string name, decimal salary)
    {
        Name = name;
        Salary = salary;
    }

    public string Name { get; }
    public decimal Salary { get; }
}

/// <summary>
/// Une équipe : un manager, et des membres hétérogènes stockés "en vrac".
/// </summary>
public sealed class Team
{
    private readonly List<object> _members = new();

    public Team(string name, decimal managerSalary)
    {
        Name = name;
        ManagerSalary = managerSalary;
    }

    public string Name { get; }
    public decimal ManagerSalary { get; }
    public IReadOnlyList<object> Members => _members;

    public void Add(object member) => _members.Add(member);
}
