using System.Collections;
using System.Reflection;

namespace Level13.Editing.Tests.Reflection;

internal static class PatternInspector
{
    public static Assembly SystemUnderTest => typeof(Editor).Assembly;

    private const BindingFlags Members =
        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

    /// <summary>
    /// Détecte des actions réifiées et historisées : une abstraction de commande
    /// (au moins deux opérations, ex. exécuter/annuler), implémentée par au moins
    /// une commande concrète, et conservée dans une collection (l'historique).
    /// </summary>
    public static bool HasCommandHistory(Assembly assembly)
    {
        var types = LoadableTypes(assembly).ToList();

        foreach (var command in types.Where(t => t.IsInterface && t.GetMethods().Length >= 2))
        {
            var hasConcreteCommand = types.Any(t =>
                t is { IsClass: true, IsAbstract: false } && command.IsAssignableFrom(t) && t != command);

            if (hasConcreteCommand && types.Any(t => HoldsCollectionOf(t, command)))
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
