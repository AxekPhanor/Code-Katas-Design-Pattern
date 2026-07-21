using System.Reflection;

namespace Level05.Ui.Tests.Reflection;

internal static class PatternInspector
{
    public static Assembly SystemUnderTest => typeof(IButton).Assembly;

    /// <summary>
    /// Détecte une fabrique abstraite : une interface dont au moins deux
    /// méthodes renvoient des contrats (interfaces) DISTINCTS — la famille de
    /// produits — implémentée par au moins deux fabriques concrètes.
    /// </summary>
    public static bool HasAbstractFactory(Assembly assembly)
    {
        var types = LoadableTypes(assembly).ToList();

        var factories = types.Where(type =>
            type.IsInterface &&
            type.GetMethods()
                .Select(method => method.ReturnType)
                .Where(returnType => returnType.IsInterface)
                .Distinct()
                .Count() >= 2);

        return factories.Any(factory =>
            types.Count(t =>
                t is { IsClass: true, IsAbstract: false } &&
                factory.IsAssignableFrom(t) && t != factory) >= 2);
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
