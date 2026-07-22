namespace Level01.AssemblyPlant;

public interface IAssemblyFactoryBuilder
{
    public AssemblyFactoryBuilder AddConveyor(string name, int speed);
    public AssemblyFactoryBuilder AddRoboticArms(int roboticArms);
    public AssemblyFactoryBuilder AddQualityControl();
    public AssemblyFactoryBuilder AddPackagingUnit();
}