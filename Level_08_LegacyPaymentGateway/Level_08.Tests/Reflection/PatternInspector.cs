using System.Reflection;

namespace Level08.Payments.Tests.Reflection;

internal static class PatternInspector
{
    public static Assembly SystemUnderTest => typeof(IPaymentProcessor).Assembly;

    /// <summary>
    /// Détecte un adaptateur : un type qui implémente le contrat cible ET
    /// enveloppe explicitement le type incompatible (l'adapté), via un paramètre
    /// de constructeur ou un champ — au lieu de le fabriquer localement à la volée.
    /// </summary>
    public static bool HasAdapter(Assembly assembly, Type target, Type adaptee)
    {
        return LoadableTypes(assembly).Any(type =>
            type is { IsClass: true, IsAbstract: false } &&
            target.IsAssignableFrom(type) && type != target &&
            (WrapsViaConstructor(type, adaptee) || WrapsViaField(type, adaptee)));
    }

    private static bool WrapsViaConstructor(Type type, Type adaptee) =>
        type.GetConstructors()
            .Any(ctor => ctor.GetParameters().Any(p => p.ParameterType == adaptee));

    private static bool WrapsViaField(Type type, Type adaptee) =>
        type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Any(field => field.FieldType == adaptee);

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
