using System.Collections;
using System.Reflection;

namespace Level06.Organization.Tests.Reflection;

internal static class PatternInspector
{
    public static Assembly SystemUnderTest => typeof(OrgChart).Assembly;

    /// <summary>
    /// Détecte une structure composite : une abstraction commune (interface),
    /// implémentée par au moins deux types, dont l'un contient une collection de
    /// cette même abstraction (le nœud qui agrège ses enfants uniformément).
    /// </summary>
    public static bool HasCompositeStructure(Assembly assembly)
    {
        var types = LoadableTypes(assembly).ToList();

        foreach (var contract in types.Where(t => t.IsInterface))
        {
            var implementers = types
                .Where(t => t is { IsClass: true, IsAbstract: false } &&
                            contract.IsAssignableFrom(t) && t != contract)
                .ToList();

            if (implementers.Count >= 2 &&
                implementers.Any(impl => HoldsCollectionOf(impl, contract)))
            {
                return true;
            }
        }

        return false;
    }

    private static bool HoldsCollectionOf(Type type, Type element)
    {
        var memberTypes = type
            .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Select(p => p.PropertyType)
            .Concat(type
                .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Select(f => f.FieldType));

        return memberTypes.Any(memberType => IsEnumerableOf(memberType, element));
    }

    private static bool IsEnumerableOf(Type type, Type element)
    {
        if (type == typeof(string))
        {
            return false;
        }

        if (type.IsArray)
        {
            return element.IsAssignableFrom(type.GetElementType()!);
        }

        return typeof(IEnumerable).IsAssignableFrom(type) &&
               type.IsGenericType &&
               type.GetGenericArguments().Any(element.IsAssignableFrom);
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
