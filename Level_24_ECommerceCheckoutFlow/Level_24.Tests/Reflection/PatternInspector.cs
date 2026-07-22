using System.Collections;
using System.Reflection;

namespace Level24.Checkout.Tests.Reflection;

internal static class PatternInspector
{
    public static Assembly SystemUnderTest => typeof(Checkout).Assembly;

    private const BindingFlags Members =
        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

    /// <summary>
    /// Détecte une saga orchestrée : une abstraction d'étape (au moins deux
    /// opérations, ex. exécuter/compenser) implémentée par au moins deux étapes,
    /// et orchestrée via une collection de ces étapes.
    /// </summary>
    public static bool HasSagaOrchestration(Assembly assembly)
    {
        var types = LoadableTypes(assembly).ToList();

        foreach (var step in types.Where(t => t.IsInterface && t.GetMethods().Length >= 2))
        {
            var implementers = types.Count(t =>
                t is { IsClass: true, IsAbstract: false } && step.IsAssignableFrom(t) && t != step);
            if (implementers < 2)
            {
                continue;
            }

            if (types.Any(t => HoldsCollectionOf(t, step)))
            {
                return true;
            }
        }

        return false;
    }

    private static bool HoldsCollectionOf(Type type, Type element)
    {
        var memberTypes = type.GetProperties(Members).Select(p => p.PropertyType)
            .Concat(type.GetFields(Members).Select(f => f.FieldType));

        return memberTypes.Any(memberType =>
            memberType != typeof(string) &&
            typeof(IEnumerable).IsAssignableFrom(memberType) &&
            memberType.IsGenericType &&
            memberType.GetGenericArguments().Any(element.IsAssignableFrom));
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
