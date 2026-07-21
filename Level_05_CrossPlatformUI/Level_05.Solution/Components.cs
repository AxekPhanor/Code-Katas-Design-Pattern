namespace Level05.Ui;

/// <summary>Contrats stables des composants graphiques.</summary>
public interface IButton
{
    string Style { get; }
    string Render();
}

public interface ICheckbox
{
    string Style { get; }
    string Render();
}

public sealed class WindowsButton : IButton
{
    public string Style => "windows";
    public string Render() => "[ Windows Button ]";
}

public sealed class MacButton : IButton
{
    public string Style => "mac";
    public string Render() => "( Mac Button )";
}

public sealed class WindowsCheckbox : ICheckbox
{
    public string Style => "windows";
    public string Render() => "[x] Windows";
}

public sealed class MacCheckbox : ICheckbox
{
    public string Style => "mac";
    public string Render() => "(x) Mac";
}

/// <summary>Un ensemble cohérent de composants.</summary>
public sealed record UiComponents(IButton Button, ICheckbox Checkbox);
