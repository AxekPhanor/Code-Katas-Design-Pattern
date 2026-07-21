namespace Level13.Editing;

// -----------------------------------------------------------------------------
//  CODE MÉTIER À REFACTORISER
// -----------------------------------------------------------------------------
//  L'annulation est gérée en photographiant l'état complet du texte à chaque
//  opération et en empilant ces chaînes. Les ACTIONS elles-mêmes ne sont pas
//  réifiées : ajouter une nouvelle opération (supprimer, remplacer, mettre en
//  gras…) obligerait à tisser à la main sa logique d'annulation dans l'éditeur.
//
//  Objectif : encapsuler chaque action dans un objet qui sait s'exécuter ET
//  s'annuler, et laisser l'éditeur empiler ces objets pour undo/redo.
// -----------------------------------------------------------------------------
public sealed class Editor
{
    private readonly Stack<string> _undo = new();
    private readonly Stack<string> _redo = new();
    private string _text = string.Empty;

    public string Text => _text;

    public void Append(string text)
    {
        _undo.Push(_text);
        _redo.Clear();
        _text += text;
    }

    public void Undo()
    {
        if (_undo.Count == 0)
        {
            return;
        }

        _redo.Push(_text);
        _text = _undo.Pop();
    }

    public void Redo()
    {
        if (_redo.Count == 0)
        {
            return;
        }

        _undo.Push(_text);
        _text = _redo.Pop();
    }
}
