using System.Reflection;

namespace Level26.Resilience.Tests.Reflection;

internal static class PatternInspector
{
    public static Assembly SystemUnderTest => typeof(ApiClient).Assembly;

    private const BindingFlags Members =
        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

    /// <summary>
    /// Détecte un disjoncteur : un état de circuit (une énumération du projet)
    /// conservé par un client, qui lui permet de basculer et d'échouer vite.
    /// </summary>
    public static bool HasCircuitBreaker(Assembly assembly)
    {
        var types = LoadableTypes(assembly).ToList();

        var circuitStates = types.Where(t => t.IsEnum).ToList();

        return circuitStates.Any(state =>
            types.Any(t =>
                t.IsClass &&
                (t.GetFields(Members).Any(f => f.FieldType == state) ||
                 t.GetProperties(Members).Any(p => p.PropertyType == state))));
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
