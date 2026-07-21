using System.Reflection;

namespace Level04.EnergyGrid.Tests.Reflection;

internal static class PatternInspector
{
    public static Assembly SystemUnderTest => typeof(IEnergyResource).Assembly;

    /// <summary>
    /// Détecte une hiérarchie de fabriques : une classe abstraite déclarant une
    /// méthode qui renvoie le produit, redéfinie par au moins deux fabriques
    /// concrètes — au lieu d'un switch centralisé.
    /// </summary>
    public static bool HasFactoryMethodHierarchy(Assembly assembly, Type product)
    {
        var types = LoadableTypes(assembly).ToList();

        var creators = types.Where(type =>
            type is { IsClass: true, IsAbstract: true } &&
            type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Any(method => product.IsAssignableFrom(method.ReturnType)));

        return creators.Any(creator =>
            types.Count(t =>
                t is { IsClass: true, IsAbstract: false } &&
                creator.IsAssignableFrom(t) && t != creator) >= 2);
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
