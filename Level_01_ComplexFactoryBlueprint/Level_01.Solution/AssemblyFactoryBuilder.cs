namespace Level01.AssemblyPlant;

/// <summary>
/// Assemble une <see cref="IAssemblyFactory"/> étape par étape, avec une API
/// fluide. Chaque étape est optionnelle et le nombre de convoyeurs n'est plus
/// limité : on en ajoute autant que nécessaire.
/// </summary>
public sealed class AssemblyFactoryBuilder
{
    private readonly List<Conveyor> _conveyors = new();
    private string? _name;
    private int _roboticArms;
    private bool _qualityControl;

    public AssemblyFactoryBuilder Named(string name)
    {
        _name = name;
        return this;
    }

    public AssemblyFactoryBuilder AddConveyor(string name, int unitsPerMinute)
    {
        _conveyors.Add(new Conveyor(name, unitsPerMinute));
        return this;
    }

    public AssemblyFactoryBuilder WithRoboticArms(int count)
    {
        _roboticArms = count;
        return this;
    }

    public AssemblyFactoryBuilder WithQualityControl()
    {
        _qualityControl = true;
        return this;
    }

    public IAssemblyFactory Build()
    {
        if (string.IsNullOrWhiteSpace(_name))
        {
            throw new InvalidOperationException("A factory name is required.");
        }

        if (_conveyors.Count == 0)
        {
            throw new InvalidOperationException("At least one conveyor is required.");
        }

        return new AssemblyFactory(
            _name,
            _conveyors.ToArray(),
            _roboticArms,
            _qualityControl);
    }
}
