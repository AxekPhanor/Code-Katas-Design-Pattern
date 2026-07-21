namespace Level13.Editing;

/// <summary>Une action réifiée : elle sait s'exécuter ET s'annuler.</summary>
public interface ICommand
{
    void Execute();
    void Undo();
}

/// <summary>Le document manipulé par les commandes.</summary>
public sealed class Document
{
    public string Text { get; private set; } = string.Empty;

    public void Append(string text) => Text += text;

    public void RemoveLast(int length) => Text = Text[..^length];
}

public sealed class AppendCommand : ICommand
{
    private readonly Document _document;
    private readonly string _text;

    public AppendCommand(Document document, string text)
    {
        _document = document;
        _text = text;
    }

    public void Execute() => _document.Append(_text);

    public void Undo() => _document.RemoveLast(_text.Length);
}

/// <summary>
/// L'éditeur empile des commandes : undo/redo se contentent de rejouer ou
/// d'annuler la dernière. Ajouter une action = ajouter une commande, sans
/// toucher à l'éditeur.
/// </summary>
public sealed class Editor
{
    private readonly Document _document = new();
    private readonly Stack<ICommand> _undo = new();
    private readonly Stack<ICommand> _redo = new();

    public string Text => _document.Text;

    public void Append(string text)
    {
        var command = new AppendCommand(_document, text);
        command.Execute();
        _undo.Push(command);
        _redo.Clear();
    }

    public void Undo()
    {
        if (_undo.Count == 0)
        {
            return;
        }

        var command = _undo.Pop();
        command.Undo();
        _redo.Push(command);
    }

    public void Redo()
    {
        if (_redo.Count == 0)
        {
            return;
        }

        var command = _redo.Pop();
        command.Execute();
        _undo.Push(command);
    }
}
