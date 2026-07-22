using System.Collections;
using System.Reflection;

namespace Level25.Messaging.Tests.Reflection;

internal static class PatternInspector
{
    public static Assembly SystemUnderTest => typeof(OrderProcessor).Assembly;

    private const BindingFlags Members =
        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

    /// <summary>
    /// Détecte une boîte d'envoi : un type qui conserve une collection de messages
    /// dédiés (une classe du projet), persistés en attendant d'être publiés — au
    /// lieu de publier directement sur le bus.
    /// </summary>
    public static bool HasOutbox(Assembly assembly)
    {
        var types = LoadableTypes(assembly).ToList();

        return types.Any(type =>
        {
            var memberTypes = type.GetProperties(Members).Select(p => p.PropertyType)
                .Concat(type.GetFields(Members).Select(f => f.FieldType));

            return memberTypes.Any(memberType => IsCollectionOfProjectClass(memberType, assembly));
        });
    }

    private static bool IsCollectionOfProjectClass(Type type, Assembly assembly)
    {
        if (type == typeof(string) ||
            !typeof(IEnumerable).IsAssignableFrom(type) ||
            !type.IsGenericType)
        {
            return false;
        }

        return type.GetGenericArguments().Any(arg =>
            arg.Assembly == assembly && arg.IsClass && arg != typeof(string));
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
