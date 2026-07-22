using System.Reflection;

namespace Level23.Endpoints.Tests.Reflection;

internal static class PatternInspector
{
    public static Assembly SystemUnderTest => typeof(Api).Assembly;

    /// <summary>
    /// Détecte le patron requête-point d'entrée-réponse : une classe dédiée qui
    /// implémente une interface de point d'entrée dont la méthode transforme UNE
    /// requête en UNE réponse (type de retour différent du paramètre).
    /// </summary>
    public static bool HasRequestEndpointResponse(Assembly assembly)
    {
        foreach (var type in LoadableTypes(assembly).Where(t => t is { IsClass: true, IsAbstract: false }))
        {
            foreach (var contract in type.GetInterfaces().Where(i => i.Assembly == assembly))
            {
                var handle = contract.GetMethods().FirstOrDefault();
                if (handle is null)
                {
                    continue;
                }

                var parameters = handle.GetParameters();
                if (parameters.Length == 1 &&
                    handle.ReturnType != typeof(void) &&
                    handle.ReturnType != parameters[0].ParameterType)
                {
                    return true;
                }
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
