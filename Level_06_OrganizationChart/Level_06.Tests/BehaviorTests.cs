using Xunit;

namespace Level06.Organization.Tests;

public sealed class BehaviorTests
{
    private static Team SampleOrganisation()
    {
        var engineering = new Team("Engineering", 10000m);
        engineering.Add(new Employee("Alice", 5000m));

        var backend = new Team("Backend", 8000m);
        backend.Add(new Employee("Bob", 4000m));

        engineering.Add(backend);
        return engineering;
    }

    [Fact]
    public void Headcount_is_counted_recursively_through_the_hierarchy()
    {
        // Engineering manager + Alice + Backend manager + Bob = 4
        Assert.Equal(4, OrgChart.HeadCount(SampleOrganisation()));
    }

    [Fact]
    public void Monthly_cost_sums_every_salary_recursively()
    {
        // 10000 + 5000 + 8000 + 4000 = 27000
        Assert.Equal(27000m, OrgChart.MonthlyCost(SampleOrganisation()));
    }
}
