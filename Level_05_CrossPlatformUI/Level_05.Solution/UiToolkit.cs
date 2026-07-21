namespace Level05.Ui;

/// <summary>
/// Un seul point de décision : la plateforme choisit sa fabrique, qui produit
/// une famille de composants garantie cohérente.
/// </summary>
public sealed class UiToolkit
{
    private readonly Dictionary<string, IUiFactory> _factories = new()
    {
        ["windows"] = new WindowsUiFactory(),
        ["mac"] = new MacUiFactory(),
    };

    public UiComponents CreateComponents(string platform)
    {
        if (!_factories.TryGetValue(platform, out var factory))
        {
            throw new ArgumentException($"Unknown platform: {platform}", nameof(platform));
        }

        return new UiComponents(factory.CreateButton(), factory.CreateCheckbox());
    }
}
