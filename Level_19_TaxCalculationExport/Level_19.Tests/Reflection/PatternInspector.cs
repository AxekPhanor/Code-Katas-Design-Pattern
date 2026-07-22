using System.Reflection;

namespace Level19.Taxation.Tests.Reflection;

internal static class PatternInspector
{
    public static Assembly SystemUnderTest => typeof(TaxReport).Assembly;

    private const BindingFlags Members =
        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

    /// <summary>
    /// Détecte un visiteur : une interface d'opération dont au moins deux méthodes
    /// prennent chacune un TYPE concret d'élément, et une hiérarchie d'éléments
    /// qui "accepte" ce visiteur (une méthode prenant l'interface d'opération).
    /// </summary>
    public static bool HasVisitor(Assembly assembly)
    {
        var types = LoadableTypes(assembly).ToList();

        foreach (var visitor in types.Where(t => t.IsInterface))
        {
            var methods = visitor.GetMethods();

            var visitsConcreteElements =
                methods.Length >= 2 &&
                methods.All(m =>
                    m.GetParameters().Length == 1 &&
                    m.GetParameters()[0].ParameterType.IsClass &&
                    m.GetParameters()[0].ParameterType != typeof(string));

            if (!visitsConcreteElements)
            {
                continue;
            }

            var isAccepted = types.Any(t =>
                t.GetMethods(Members).Any(m =>
                    m.GetParameters().Any(p => p.ParameterType == visitor)));

            if (isAccepted)
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
