namespace Level04.EnergyGrid;

// 1. La Fabrique Abstraite
public abstract class EnergyResourceFactory
{
    // Remarque : plus besoin de paramètre "string kind" !
    public abstract IEnergyResource Create(); 
}

// 2. La Fabrique pour le Solaire
public class SolarResourceFactory : EnergyResourceFactory
{
    public override IEnergyResource Create()
    {
        return new SolarResource();
    }
}

// 3. La Fabrique pour l'Éolien (Wind)
public class WindResourceFactory : EnergyResourceFactory
{
    public override IEnergyResource Create()
    {
        return new WindResource();
    }
}

// 4. La Fabrique pour le Nucléaire
public class NuclearResourceFactory : EnergyResourceFactory
{
    public override IEnergyResource Create()
    {
        return new NuclearResource();
    }
}