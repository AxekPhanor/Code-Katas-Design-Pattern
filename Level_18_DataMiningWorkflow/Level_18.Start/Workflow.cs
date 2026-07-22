namespace Level18.DataMining;

// -----------------------------------------------------------------------------
//  CODE MÉTIER À REFACTORISER
// -----------------------------------------------------------------------------
//  Le squelette de l'algorithme (extraire -> analyser -> conclure) est RECOPIÉ
//  dans chaque branche du switch, une fois par format. Seules deux étapes varient,
//  mais tout le déroulé est dupliqué. Changer l'ordre ou l'étape finale oblige à
//  modifier chaque copie.
//
//  Objectif : figer le squelette de l'algorithme UNE seule fois, et ne laisser
//  redéfinissables que les étapes qui varient d'un format à l'autre.
// -----------------------------------------------------------------------------
public static class Workflow
{
    public static string Run(string format)
    {
        switch (format)
        {
            case "csv":
            {
                var raw = "a,b,c";
                var parsed = $"CSV[{raw.Replace(",", "|")}]";
                return $"Analyzed {parsed}";
            }

            case "pdf":
            {
                var raw = "%PDF hello";
                var parsed = $"PDF[{raw.Replace("%PDF ", string.Empty)}]";
                return $"Analyzed {parsed}";
            }

            default:
                throw new ArgumentException($"Unknown format: {format}", nameof(format));
        }
    }
}
