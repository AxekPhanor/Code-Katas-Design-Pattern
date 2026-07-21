using System.Collections;
using System.Reflection;

namespace Level15.Logistics.Tests.Reflection;

internal static class PatternInspector
{
    public static Assembly SystemUnderTest => typeof(RoutePlanner).Assembly;

    private const BindingFlags Members =
        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

    /// <summary>
    /// Détecte des stratégies interchangeables : une interface d'algorithme
    /// implémentée par au moins deux stratégies concrètes, et référencée par un
    /// contexte (champ de ce type, ou collection de ce type) qui lui délègue.
    /// </summary>
    public static bool HasStrategy(Assembly assembly)
    {
        var types = LoadableTypes(assembly).ToList();

        foreach (var strategy in types.Where(t => t.IsInterface && t.GetMethods().Length >= 1))
        {
            var implementers = types.Count(t =>
                t is { IsClass: true, IsAbstract: false } && strategy.IsAssignableFrom(t) && t != strategy);

            if (implementers < 2)
            {
                continue;
            }

            var usedByContext = types.Any(t =>
                !strategy.IsAssignableFrom(t) && ReferencesStrategy(t, strategy));

            if (usedByContext)
            {
                return true;
            }
        }

        return false;
    }

    private static bool ReferencesStrategy(Type type, Type strategy)
    {
        var memberTypes = type.GetProperties(Members).Select(p => p.PropertyType)
            .Concat(type.GetFields(Members).Select(f => f.FieldType));

        return memberTypes.Any(memberType =>
            memberType == strategy ||
            (memberType != typeof(string) &&
             typeof(IEnumerable).IsAssignableFrom(memberType) &&
             memberType.IsGenericType &&
             memberType.GetGenericArguments().Contains(strategy)));
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
