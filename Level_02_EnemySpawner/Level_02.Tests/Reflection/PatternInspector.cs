using System.Reflection;

namespace Level02.Bestiary.Tests.Reflection;

/// <summary>
/// Analyse de forme (agnostique aux noms) pour ce niveau.
/// </summary>
internal static class PatternInspector
{
    public static Assembly SystemUnderTest => typeof(IEnemy).Assembly;

    /// <summary>
    /// Détecte un produit capable de se copier lui-même : un type implémentant
    /// le contrat et exposant une méthode d'instance sans paramètre qui renvoie
    /// une nouvelle instance du contrat.
    /// </summary>
    public static bool HasSelfCloningProduct(Assembly assembly, Type productContract)
    {
        return LoadableTypes(assembly).Any(type =>
            type is { IsClass: true, IsAbstract: false } &&
            productContract.IsAssignableFrom(type) &&
            type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Any(method =>
                    method.GetParameters().Length == 0 &&
                    method.ReturnType != typeof(void) &&
                    productContract.IsAssignableFrom(method.ReturnType)));
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
