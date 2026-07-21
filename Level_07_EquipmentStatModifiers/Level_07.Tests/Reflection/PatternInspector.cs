using System.Reflection;

namespace Level07.Equipment.Tests.Reflection;

internal static class PatternInspector
{
    public static Assembly SystemUnderTest => typeof(IEquipment).Assembly;

    /// <summary>
    /// Détecte un décorateur : un type qui implémente le contrat ET enveloppe
    /// une autre instance du même contrat (via un paramètre de constructeur ou
    /// un champ), afin d'empiler dynamiquement des comportements.
    /// </summary>
    public static bool HasDecorator(Assembly assembly, Type component)
    {
        return LoadableTypes(assembly).Any(type =>
            type is { IsClass: true, IsAbstract: false } &&
            component.IsAssignableFrom(type) &&
            (WrapsViaConstructor(type, component) || WrapsViaField(type, component)));
    }

    private static bool WrapsViaConstructor(Type type, Type component) =>
        type.GetConstructors()
            .Any(ctor => ctor.GetParameters()
                .Any(parameter => component.IsAssignableFrom(parameter.ParameterType)));

    private static bool WrapsViaField(Type type, Type component) =>
        type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Any(field => component.IsAssignableFrom(field.FieldType));

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
