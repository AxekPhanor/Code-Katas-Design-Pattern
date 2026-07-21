using System.Reflection;

namespace Level09.HomeTheater.Tests.Reflection;

internal static class PatternInspector
{
    public static Assembly SystemUnderTest => typeof(ISubsystem).Assembly;

    /// <summary>
    /// Détecte une façade : un type qui n'est pas lui-même un sous-système, mais
    /// qui agrège au moins trois sous-systèmes (via des champs) pour les
    /// orchestrer derrière une opération simplifiée.
    /// </summary>
    public static bool HasFacade(Assembly assembly, Type subsystemMarker)
    {
        return LoadableTypes(assembly).Any(type =>
            type is { IsClass: true, IsAbstract: false } &&
            !subsystemMarker.IsAssignableFrom(type) &&
            type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Count(field => subsystemMarker.IsAssignableFrom(field.FieldType)) >= 3);
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
