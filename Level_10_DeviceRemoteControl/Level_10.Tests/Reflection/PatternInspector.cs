using System.Reflection;

namespace Level10.Devices.Tests.Reflection;

internal static class PatternInspector
{
    public static Assembly SystemUnderTest => typeof(IDevice).Assembly;

    /// <summary>
    /// Détecte un pont : l'implémenteur (IDevice) expose une vraie interface de
    /// commandes (au moins deux opérations), et une abstraction distincte
    /// (qui n'est pas un appareil) le référence pour lui déléguer.
    /// </summary>
    public static bool HasBridge(Assembly assembly, Type implementor)
    {
        if (implementor.GetMethods().Length < 2)
        {
            return false;
        }

        return LoadableTypes(assembly).Any(type =>
            type.IsClass &&
            !implementor.IsAssignableFrom(type) &&
            (ReferencesViaConstructor(type, implementor) || ReferencesViaField(type, implementor)));
    }

    private static bool ReferencesViaConstructor(Type type, Type implementor) =>
        type.GetConstructors()
            .Any(ctor => ctor.GetParameters().Any(p => implementor.IsAssignableFrom(p.ParameterType)));

    private static bool ReferencesViaField(Type type, Type implementor) =>
        type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Any(field => implementor.IsAssignableFrom(field.FieldType));

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
