using System.Reflection;

namespace Level16.Combat.Tests.Reflection;

internal static class PatternInspector
{
    public static Assembly SystemUnderTest => typeof(Character).Assembly;

    private const BindingFlags Members =
        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

    /// <summary>
    /// Détecte une machine à états objet : une interface d'état dont plusieurs
    /// méthodes renvoient CE MÊME type (les transitions), implémentée par au
    /// moins deux états, et détenue par un contexte qui lui délègue.
    /// </summary>
    public static bool HasStateMachine(Assembly assembly)
    {
        var types = LoadableTypes(assembly).ToList();

        foreach (var state in types.Where(t => t.IsInterface))
        {
            var transitions = state.GetMethods().Count(m => m.ReturnType == state);
            if (transitions < 2)
            {
                continue;
            }

            var implementers = types.Count(t =>
                t is { IsClass: true, IsAbstract: false } && state.IsAssignableFrom(t) && t != state);
            if (implementers < 2)
            {
                continue;
            }

            var heldByContext = types.Any(t =>
                !state.IsAssignableFrom(t) &&
                (t.GetFields(Members).Any(f => f.FieldType == state) ||
                 t.GetProperties(Members).Any(p => p.PropertyType == state)));

            if (heldByContext)
            {
                return true;
            }
        }

        return false;
    }

    private static IEnumerable<Type> LoadableTypes(Assembly assembly)
    {
        try
        {
            return assembly.GetTypes();
        }
        catch (ReflectionTypeLoadException ex)
        {
            return ex.Types.OfType<Type>();
        }
    }
}
