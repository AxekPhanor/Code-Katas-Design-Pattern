namespace Level01.AssemblyPlant;

public sealed class AssemblyFactoryBuilder : IAssemblyFactoryBuilder
{
    internal readonly List<Conveyor> conveyors = new();
    internal int roboticArms = 0;
    internal bool qualityControl = false;
    internal bool packagingUnit = false;
    private AssemblyFactory _assemblyFactory;

    public AssemblyFactoryBuilder AddConveyor(string name, int speed)
    {        
        conveyors.Add(new(name, speed));
        return this;
    }

    public AssemblyFactoryBuilder AddPackagingUnit()
    {
        packagingUnit = true;
        return this;
    }

    public AssemblyFactoryBuilder AddQualityControl()
    {
        qualityControl = true;
        return this;
    }

    public AssemblyFactoryBuilder AddRoboticArms(int roboticArms)
    {
        this.roboticArms += roboticArms;
        return this;
    }

    public AssemblyFactory Build(string name)
    {
        _assemblyFactory = new(name, this);
        return _assemblyFactory;
    }
}