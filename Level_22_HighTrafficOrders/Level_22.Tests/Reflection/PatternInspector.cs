using System.Reflection;

namespace Level22.Orders.Tests.Reflection;

internal static class PatternInspector
{
    public static Assembly SystemUnderTest => typeof(OrderApi).Assembly;

    /// <summary>
    /// Détecte la séparation commande/requête : un gestionnaire d'ÉCRITURE (une
    /// interface dont la méthode ne renvoie rien) ET un gestionnaire de LECTURE
    /// distinct (une interface dont la méthode renvoie un résultat), tous deux
    /// implémentés.
    /// </summary>
    public static bool HasCommandQuerySeparation(Assembly assembly)
    {
        var types = LoadableTypes(assembly).ToList();

        var hasWriteHandler = false;
        var hasReadHandler = false;

        foreach (var contract in types.Where(t => t.IsInterface))
        {
            var handle = contract.GetMethods().FirstOrDefault(m => m.GetParameters().Length == 1);
            if (handle is null)
            {
                continue;
            }

            var implemented = types.Any(t =>
                t is { IsClass: true, IsAbstract: false } && contract.IsAssignableFrom(t) && t != contract);
            if (!implemented)
            {
                continue;
            }

            if (handle.ReturnType == typeof(void))
            {
                hasWriteHandler = true;
            }
            else
            {
                hasReadHandler = true;
            }
        }

        return hasWriteHandler && hasReadHandler;
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
