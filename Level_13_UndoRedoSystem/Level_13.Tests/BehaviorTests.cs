using Xunit;

namespace Level13.Editing.Tests;

public sealed class BehaviorTests
{
    [Fact]
    public void Appending_builds_up_the_text()
    {
        var editor = new Editor();

        editor.Append("Hello");
        editor.Append(" World");

        Assert.Equal("Hello World", editor.Text);
    }

    [Fact]
    public void Undo_reverts_the_last_action()
    {
        var editor = new Editor();
        editor.Append("Hello");
        editor.Append(" World");

        editor.Undo();

        Assert.Equal("Hello", editor.Text);
    }

    [Fact]
    public void Redo_replays_an_undone_action()
    {
        var editor = new Editor();
        editor.Append("Hello");
        editor.Append(" World");

        editor.Undo();
        editor.Redo();

        Assert.Equal("Hello World", editor.Text);
    }

    [Fact]
    public void Several_actions_can_be_undone_in_reverse_order()
    {
        var editor = new Editor();
        editor.Append("A");
        editor.Append("B");
        editor.Append("C");

        editor.Undo();
        editor.Undo();

        Assert.Equal("A", editor.Text);
    }
}
