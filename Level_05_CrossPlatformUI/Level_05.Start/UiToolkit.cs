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
    public UiComponents CreateComponents(string platform)
    {
        IButton button = platform switch
        {
            "windows" => new WindowsButton(),
            "mac" => new MacButton(),
            _ => throw new ArgumentException($"Unknown platform: {platform}", nameof(platform)),
        };

        ICheckbox checkbox = platform switch
        {
            "windows" => new WindowsCheckbox(),
            "mac" => new MacCheckbox(),
            _ => throw new ArgumentException($"Unknown platform: {platform}", nameof(platform)),
        };

        return new UiComponents(button, checkbox);
    }
}
