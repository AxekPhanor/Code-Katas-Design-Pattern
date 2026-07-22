namespace Level18.DataMining;

/// <summary>
/// Le squelette de l'algorithme est figé ici (la "méthode template"). Seules
/// les étapes qui varient sont abstraites et redéfinies par les sous-classes.
/// </summary>
public abstract class DataMiner
{
    public string Mine()
    {
        var raw = ExtractData();
        var parsed = ParseData(raw);
        return $"Analyzed {parsed}";
    }

    protected abstract string ExtractData();

    protected abstract string ParseData(string raw);
}

public sealed class CsvDataMiner : DataMiner
{
    protected override string ExtractData() => "a,b,c";

    protected override string ParseData(string raw) => $"CSV[{raw.Replace(",", "|")}]";
}

public sealed class PdfDataMiner : DataMiner
{
    protected override string ExtractData() => "%PDF hello";

    protected override string ParseData(string raw) => $"PDF[{raw.Replace("%PDF ", string.Empty)}]";
}

public static class Workflow
{
    public static string Run(string format) => format switch
    {
        "csv" => new CsvDataMiner().Mine(),
        "pdf" => new PdfDataMiner().Mine(),
        _ => throw new ArgumentException($"Unknown format: {format}", nameof(format)),
    };
}
