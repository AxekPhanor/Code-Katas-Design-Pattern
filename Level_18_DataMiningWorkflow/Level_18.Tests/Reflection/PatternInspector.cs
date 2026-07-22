using System.Reflection;

namespace Level18.DataMining.Tests.Reflection;

internal static class PatternInspector
{
    public static Assembly SystemUnderTest => typeof(Workflow).Assembly;

    private const BindingFlags DeclaredMembers =
        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;

    /// <summary>
    /// Détecte une méthode template : une classe abstraite qui expose au moins
    /// une méthode CONCRÈTE (le squelette figé) et au moins une méthode ABSTRAITE
    /// (une étape redéfinissable), avec au moins deux sous-classes concrètes.
    /// </summary>
    public static bool HasTemplateMethod(Assembly assembly)
    {
        var types = LoadableTypes(assembly).ToList();

        foreach (var baseType in types.Where(t => t.IsClass && t.IsAbstract))
        {
            var declaredMethods = baseType.GetMethods(DeclaredMembers)
                .Where(m => !m.IsSpecialName)
                .ToList();

            var hasConcreteSkeleton = declaredMethods.Any(m => !m.IsAbstract);
            var hasAbstractStep = declaredMethods.Any(m => m.IsAbstract);

            var concreteSubclasses = types.Count(t =>
                t is { IsClass: true, IsAbstract: false } && baseType.IsAssignableFrom(t) && t != baseType);

            if (hasConcreteSkeleton && hasAbstractStep && concreteSubclasses >= 2)
            {
                return true;
            }
        }

        return false;
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
