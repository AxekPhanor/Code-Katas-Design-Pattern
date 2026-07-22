using System.Reflection;

namespace Level17.Campfire.Tests.Reflection;

internal static class PatternInspector
{
    public static Assembly SystemUnderTest => typeof(Player).Assembly;

    private const BindingFlags PublicInstance = BindingFlags.Public | BindingFlags.Instance;

    /// <summary>
    /// Détecte un memento : un type que l'originateur RENVOIE (Save) ET REÇOIT
    /// (Restore), et dont l'état est encapsulé — aucun membre public modifiable,
    /// donc impossible à lire ou forger de l'extérieur.
    /// </summary>
    public static bool HasMemento(Assembly assembly, Type originator)
    {
        var methods = originator.GetMethods(PublicInstance);

        var returned = methods
            .Select(m => m.ReturnType)
            .Where(t => t.IsClass && t != typeof(string));

        var accepted = methods
            .SelectMany(m => m.GetParameters().Select(p => p.ParameterType))
            .Where(t => t.IsClass && t != typeof(string));

        var mementoCandidates = returned.Intersect(accepted);

        return mementoCandidates.Any(IsEncapsulated);
    }

    private static bool IsEncapsulated(Type memento)
    {
        var hasPublicWritableProperty = memento.GetProperties(PublicInstance)
            .Any(p => p.SetMethod is { IsPublic: true });

        var hasPublicField = memento.GetFields(PublicInstance).Length > 0;

        return !hasPublicWritableProperty && !hasPublicField;
    }
}
