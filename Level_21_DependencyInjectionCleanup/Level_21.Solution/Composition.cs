namespace Level21.Composition;

/// <summary>Un registre de services minimal (par nom).</summary>
public sealed class ServiceRegistry
{
    private readonly HashSet<string> _services = new();

    public void Add(string service) => _services.Add(service);

    public bool Contains(string service) => _services.Contains(service);

    public int Count => _services.Count;
}

/// <summary>Module Paiement : cohésif, exposé par une extension du registre.</summary>
public static class PaymentModule
{
    public static ServiceRegistry AddPaymentModule(this ServiceRegistry services)
    {
        services.Add("PaymentGateway");
        services.Add("RefundService");
        return services;
    }
}

public static class NotificationModule
{
    public static ServiceRegistry AddNotificationModule(this ServiceRegistry services)
    {
        services.Add("EmailSender");
        services.Add("SmsSender");
        return services;
    }
}

public static class OrderModule
{
    public static ServiceRegistry AddOrderModule(this ServiceRegistry services)
    {
        services.Add("OrderRepository");
        services.Add("OrderValidator");
        return services;
    }
}

public static class Startup
{
    public static void ConfigureServices(ServiceRegistry services)
    {
        services
            .AddPaymentModule()
            .AddNotificationModule()
            .AddOrderModule();
    }
}
