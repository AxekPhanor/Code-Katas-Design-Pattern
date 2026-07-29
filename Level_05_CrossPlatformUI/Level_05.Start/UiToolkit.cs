namespace Level05.Ui;

// -----------------------------------------------------------------------------
//  CODE MÉTIER À REFACTORISER
// -----------------------------------------------------------------------------
//  Chaque composant est choisi par un `switch` INDÉPENDANT sur la plateforme.
//  Rien ne garantit la cohérence de la famille : un oubli et on se retrouve
//  avec un bouton Windows et une case à cocher Mac. Ajouter une plateforme
//  oblige à modifier tous les switches, un par composant.
//
//  Objectif : garantir qu'une plateforme produise TOUJOURS une famille de
//  composants cohérente, via un seul point de décision.
// -----------------------------------------------------------------------------
public sealed class UiToolkit
{
    private readonly Dictionary<string, IComponentsFactory> _factories = new()
    {
            ["windows"] = new WindowsFactory(),
            ["mac"] = new MacFactory()
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
