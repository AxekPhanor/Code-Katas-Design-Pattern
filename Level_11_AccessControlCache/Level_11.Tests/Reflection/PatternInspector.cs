using System.Reflection;

namespace Level11.AccessControl.Tests.Reflection;

internal static class PatternInspector
{
    public static Assembly SystemUnderTest => typeof(IUserRepository).Assembly;

    /// <summary>
    /// Détecte un substitut : un type qui présente le même contrat que le sujet
    /// réel ET enveloppe une autre instance de ce contrat (via un paramètre de
    /// constructeur ou un champ), pour s'intercaler devant lui.
    /// </summary>
    public static bool HasProxy(Assembly assembly, Type subject)
    {
        return LoadableTypes(assembly).Any(type =>
            type is { IsClass: true, IsAbstract: false } &&
            subject.IsAssignableFrom(type) && type != subject &&
            (WrapsViaConstructor(type, subject) || WrapsViaField(type, subject)));
    }

    private static bool WrapsViaConstructor(Type type, Type subject) =>
        type.GetConstructors()
            .Any(ctor => ctor.GetParameters().Any(p => subject.IsAssignableFrom(p.ParameterType)));

    private static bool WrapsViaField(Type type, Type subject) =>
        type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Any(field => subject.IsAssignableFrom(field.FieldType));

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
