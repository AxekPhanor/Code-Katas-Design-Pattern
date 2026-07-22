using System.Reflection;
using System.Runtime.CompilerServices;

namespace Level21.Composition.Tests.Reflection;

internal static class PatternInspector
{
    public static Assembly SystemUnderTest => typeof(ServiceRegistry).Assembly;

    /// <summary>
    /// Détecte des enregistrements modulaires : au moins deux méthodes
    /// d'extension (static, `this`) prenant le registre en premier paramètre —
    /// chacune enregistrant un module cohérent — plutôt qu'une méthode unique.
    /// </summary>
    public static bool HasModularRegistrationExtensions(Assembly assembly, Type registry)
    {
        var extensionCount = LoadableTypes(assembly)
            .Where(t => t is { IsClass: true, IsAbstract: true, IsSealed: true }) // classe statique
            .SelectMany(t => t.GetMethods(BindingFlags.Public | BindingFlags.Static))
            .Count(m =>
                m.IsDefined(typeof(ExtensionAttribute), inherit: false) &&
                m.GetParameters().Length >= 1 &&
                m.GetParameters()[0].ParameterType == registry);

        return extensionCount >= 2;
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
