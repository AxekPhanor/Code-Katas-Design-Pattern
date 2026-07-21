using System.Reflection;

namespace Level12.Support.Tests.Reflection;

internal static class PatternInspector
{
    public static Assembly SystemUnderTest => typeof(SupportDesk).Assembly;

    private const BindingFlags Members =
        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

    /// <summary>
    /// Détecte une chaîne de responsabilité : une abstraction de gestionnaire
    /// (interface ou classe abstraite) dont un maillon référence un SUCCESSEUR
    /// du même type — le chaînage qui remplace la cascade de if/else.
    /// </summary>
    public static bool HasHandlerChain(Assembly assembly)
    {
        var types = LoadableTypes(assembly).ToList();

        foreach (var handler in types.Where(t => t.IsInterface || (t.IsClass && t.IsAbstract)))
        {
            var hasImplementers = types.Any(t =>
                t is { IsClass: true } && handler.IsAssignableFrom(t) && t != handler);

            if (!hasImplementers)
            {
                continue;
            }

            var holdsSuccessor = types
                .Where(t => handler.IsAssignableFrom(t))
                .Any(t =>
                    t.GetFields(Members).Any(f => handler.IsAssignableFrom(f.FieldType)) ||
                    t.GetProperties(Members).Any(p => handler.IsAssignableFrom(p.PropertyType)));

            if (holdsSuccessor)
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
