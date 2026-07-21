namespace Level05.Ui;

/// <summary>
/// Fabrique une famille cohérente de composants pour une plateforme donnée.
/// Chaque plateforme a sa fabrique : impossible de mélanger les styles.
/// </summary>
public interface IUiFactory
{
    IButton CreateButton();
    ICheckbox CreateCheckbox();
}

public sealed class WindowsUiFactory : IUiFactory
{
    public IButton CreateButton() => new WindowsButton();
    public ICheckbox CreateCheckbox() => new WindowsCheckbox();
}

public sealed class MacUiFactory : IUiFactory
{
    public IButton CreateButton() => new MacButton();
    public ICheckbox CreateCheckbox() => new MacCheckbox();
}
