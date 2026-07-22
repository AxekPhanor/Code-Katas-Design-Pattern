using System.Collections;
using System.Reflection;

namespace Level20.Configuration.Tests.Reflection;

internal static class PatternInspector
{
    public static Assembly SystemUnderTest => typeof(EmailService).Assembly;

    private const BindingFlags PublicInstance = BindingFlags.Public | BindingFlags.Instance;

    private const BindingFlags AnyInstance =
        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

    /// <summary>
    /// Détecte le patron Options : un objet de configuration FORTEMENT TYPÉ
    /// (une classe de données du projet, au moins deux propriétés, pas une
    /// collection) détenu par un service — au lieu de lire des clés de chaîne.
    /// </summary>
    public static bool HasStronglyTypedOptions(Assembly assembly)
    {
        var types = LoadableTypes(assembly).ToList();

        var optionTypes = types.Where(t =>
            t is { IsClass: true, IsAbstract: false } &&
            !typeof(IEnumerable).IsAssignableFrom(t) &&
            t.GetProperties(PublicInstance).Count(p => p.CanRead) >= 2).ToList();

        return optionTypes.Any(options =>
            types.Any(t => t != options &&
                t.GetFields(AnyInstance).Any(f => f.FieldType == options)));
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
