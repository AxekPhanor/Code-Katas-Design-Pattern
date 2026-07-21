using System.Reflection;

namespace Level01.AssemblyPlant.Tests.Reflection;

/// <summary>
/// Petit utilitaire de réflexion utilisé par les tests d'architecture. Il
/// raisonne sur la *forme* du code, jamais sur les noms de classes : le joueur
/// est libre de nommer ses types comme il le souhaite.
/// </summary>
internal static class PatternInspector
{
    /// <summary>
    /// L'assembly analysée = celle qui contient le contrat métier. Elle vaut
    /// <c>.Start</c> pour l'arbitre et <c>.Solution</c> pour le garde-fou de
    /// référence, selon le projet de test qui référence ce fichier.
    /// </summary>
    public static Assembly SystemUnderTest => typeof(IAssemblyFactory).Assembly;

    /// <summary>
    /// Détecte un "assembleur progressif" : un type public exposant au moins
    /// deux méthodes fluides (qui se renvoient elles-mêmes pour permettre le
    /// chaînage) et une méthode de finalisation qui produit le contrat attendu.
    /// </summary>
    public static bool HasStepwiseAssembler(Assembly assembly, Type productContract)
    {
        return LoadableTypes(assembly).Any(type =>
            type is { IsClass: true, IsAbstract: false, IsPublic: true } &&
            FluentMethodCount(type) >= 2 &&
            HasFinishingMethod(type, productContract));
    }

    /// <summary>
    /// Repère l'anti-patron du "constructeur télescopique" : tout type public
    /// implémentant le contrat et exposant un constructeur public avec plus de
    /// <paramref name="maxParameters"/> paramètres.
    /// </summary>
    public static IReadOnlyList<ConstructorInfo> TelescopingConstructors(
        Assembly assembly, Type productContract, int maxParameters)
    {
        return LoadableTypes(assembly)
            .Where(type => type is { IsClass: true, IsAbstract: false } &&
                           productContract.IsAssignableFrom(type))
            .SelectMany(type => type.GetConstructors(BindingFlags.Public | BindingFlags.Instance))
            .Where(constructor => constructor.GetParameters().Length > maxParameters)
            .ToList();
    }

    private static int FluentMethodCount(Type type) =>
        type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
            .Count(method => method.ReturnType == type);

    private static bool HasFinishingMethod(Type type, Type productContract) =>
        type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
            .Any(method => method.ReturnType != type &&
                           productContract.IsAssignableFrom(method.ReturnType));

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
