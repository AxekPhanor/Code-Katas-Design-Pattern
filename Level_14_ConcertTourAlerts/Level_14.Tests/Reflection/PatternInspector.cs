using System.Collections;
using System.Reflection;

namespace Level14.Touring.Tests.Reflection;

internal static class PatternInspector
{
    public static Assembly SystemUnderTest => typeof(ConcertTour).Assembly;

    private const BindingFlags Members =
        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

    /// <summary>
    /// Détecte un registre d'observateurs découplé : un sujet qui conserve une
    /// collection typée par l'ABSTRACTION d'abonné (pas par un type concret),
    /// afin de notifier n'importe quel abonné sans se coupler à sa classe.
    /// </summary>
    public static bool HasObserverRegistry(Assembly assembly)
    {
        var types = LoadableTypes(assembly).ToList();

        foreach (var subscriber in types.Where(t => t.IsInterface && t.GetMethods().Length >= 1))
        {
            var hasImplementer = types.Any(t =>
                t is { IsClass: true } && subscriber.IsAssignableFrom(t) && t != subscriber);

            if (hasImplementer && types.Any(t => HoldsCollectionOfExactly(t, subscriber)))
            {
                return true;
            }
        }

        return false;
    }

    private static bool HoldsCollectionOfExactly(Type type, Type element)
    {
        var memberTypes = type.GetProperties(Members).Select(p => p.PropertyType)
            .Concat(type.GetFields(Members).Select(f => f.FieldType));

        return memberTypes.Any(memberType =>
            memberType != typeof(string) &&
            typeof(IEnumerable).IsAssignableFrom(memberType) &&
            memberType.IsGenericType &&
            memberType.GetGenericArguments().Contains(element));
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
