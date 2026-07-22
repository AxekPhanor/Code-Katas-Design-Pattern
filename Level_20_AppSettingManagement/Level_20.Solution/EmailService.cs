namespace Level20.Configuration;

/// <summary>
/// Configuration SMTP fortement typée : liée une seule fois depuis la source,
/// puis manipulée en toute sûreté via ses propriétés.
/// </summary>
public sealed class SmtpOptions
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public bool UseSsl { get; set; }
}

public sealed class EmailService
{
    private readonly SmtpOptions _options;

    public EmailService(IReadOnlyDictionary<string, string> config)
    {
        _options = new SmtpOptions
        {
            Host = config["Smtp:Host"],
            Port = int.Parse(config["Smtp:Port"]),
            UseSsl = bool.Parse(config["Smtp:UseSsl"]),
        };
    }

    public string Describe() =>
        $"{_options.Host}:{_options.Port}{(_options.UseSsl ? " (SSL)" : string.Empty)}";
}
