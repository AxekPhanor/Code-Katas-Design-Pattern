namespace Level09.HomeTheater;

/// <summary>Un sous-système du home cinéma.</summary>
public interface ISubsystem
{
    string PowerOn();
}

public sealed class Amplifier : ISubsystem
{
    public string PowerOn() => "Amplifier on";
}

public sealed class Projector : ISubsystem
{
    public string PowerOn() => "Projector on";
}

public sealed class Screen : ISubsystem
{
    public string PowerOn() => "Screen on";
}

public sealed class Lights : ISubsystem
{
    public string PowerOn() => "Lights on";
}

public sealed class DvdPlayer : ISubsystem
{
    public string PowerOn() => "DvdPlayer on";
}

public sealed class PopcornMaker : ISubsystem
{
    public string PowerOn() => "PopcornMaker on";
}
