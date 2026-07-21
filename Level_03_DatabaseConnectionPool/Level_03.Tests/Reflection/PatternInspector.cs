using System.Reflection;

namespace Level03.Persistence.Tests.Reflection;

/// <summary>
/// Analyse de forme (agnostique aux noms) pour ce niveau.
/// </summary>
internal static class PatternInspector
{
    /// <summary>
    /// Un type est "protégé en instance unique" s'il n'expose aucun constructeur
    /// public et fournit un accesseur statique renvoyant sa propre instance.
    /// </summary>
    public static bool IsSingleGuardedInstance(Type type)
    {
        var noPublicConstructor =
            type.GetConstructors(BindingFlags.Public | BindingFlags.Instance).Length == 0;

        var hasStaticSelfAccessor =
            type.GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Any(method => method.ReturnType == type)
            || type.GetProperties(BindingFlags.Public | BindingFlags.Static)
                .Any(property => property.PropertyType == type);

        return noPublicConstructor && hasStaticSelfAccessor;
    }
}
