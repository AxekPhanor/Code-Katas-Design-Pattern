using Xunit;

namespace Level01.AssemblyPlant.Tests;

/// <summary>
/// Vérifie que l'usine produit toujours le bon résultat métier. Ces tests
/// doivent rester verts après ta refactorisation.
/// </summary>
public sealed class BehaviorTests
{
    private static FactorySpecification SampleSpecification() => new()
    {
        Name = "Main Line",
        Conveyors = new[]
        {
            new Conveyor("Intake", 60),
            new Conveyor("Packing", 40),
        },
        RoboticArms = 2,
        HasQualityControl = true,
    };

    [Fact]
    public void Assemble_preserves_the_factory_name()
    {
        var factory = AssemblyPlant.Assemble(SampleSpecification());

        Assert.Equal("Main Line", factory.Name);
    }

    [Fact]
    public void Assemble_keeps_every_conveyor_in_order()
    {
        var factory = AssemblyPlant.Assemble(SampleSpecification());

        Assert.Equal(2, factory.Conveyors.Count);
        Assert.Equal("Intake", factory.Conveyors[0].Name);
        Assert.Equal("Packing", factory.Conveyors[1].Name);
    }

    [Fact]
    public void Assemble_carries_the_robotic_arms_and_quality_control_flags()
    {
        var factory = AssemblyPlant.Assemble(SampleSpecification());

        Assert.Equal(2, factory.RoboticArms);
        Assert.True(factory.HasQualityControl);
    }

    [Fact]
    public void Estimated_throughput_is_driven_by_the_bottleneck_and_robotic_arms()
    {
        var factory = AssemblyPlant.Assemble(SampleSpecification());

        // Goulot d'étranglement = min(60, 40) = 40 ; + 2 bras * 5 = 50.
        Assert.Equal(50, factory.EstimatedThroughput());
    }

    [Fact]
    public void A_factory_requires_at_least_one_conveyor()
    {
        var specification = SampleSpecification();
        specification.Conveyors = Array.Empty<Conveyor>();

        Assert.ThrowsAny<Exception>(() => AssemblyPlant.Assemble(specification));
    }

    [Fact]
    public void The_line_can_be_extended_beyond_three_conveyors()
    {
        var specification = SampleSpecification();
        specification.Conveyors = new[]
        {
            new Conveyor("Intake", 60),
            new Conveyor("Weld", 55),
            new Conveyor("Paint", 50),
            new Conveyor("Packing", 40),
        };

        var factory = AssemblyPlant.Assemble(specification);

        Assert.Equal(4, factory.Conveyors.Count);
        Assert.Equal("Packing", factory.Conveyors[3].Name);
    }
}
