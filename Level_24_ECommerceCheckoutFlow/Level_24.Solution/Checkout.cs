namespace Level24.Checkout;

/// <summary>Une étape autonome de la transaction : elle sait s'exécuter et se compenser.</summary>
public interface ISagaStep
{
    void Execute(List<string> log);
    void Compensate(List<string> log);
}

public sealed class ReserveStockStep : ISagaStep
{
    public void Execute(List<string> log) => log.Add("Stock reserved");
    public void Compensate(List<string> log) => log.Add("Stock released");
}

public sealed class ChargePaymentStep : ISagaStep
{
    private readonly bool _succeeds;

    public ChargePaymentStep(bool succeeds) => _succeeds = succeeds;

    public void Execute(List<string> log)
    {
        if (!_succeeds)
        {
            throw new InvalidOperationException("Payment failed.");
        }

        log.Add("Payment charged");
    }

    public void Compensate(List<string> log) => log.Add("Payment refunded");
}

public sealed class ScheduleShippingStep : ISagaStep
{
    public void Execute(List<string> log) => log.Add("Shipping scheduled");
    public void Compensate(List<string> log) => log.Add("Shipping cancelled");
}

/// <summary>
/// Orchestrateur : exécute les étapes dans l'ordre ; à la première qui échoue,
/// compense les étapes déjà réussies dans l'ordre inverse.
/// </summary>
public sealed class CheckoutOrchestrator
{
    private readonly IReadOnlyList<ISagaStep> _steps;

    public CheckoutOrchestrator(IReadOnlyList<ISagaStep> steps) => _steps = steps;

    public IReadOnlyList<string> Run()
    {
        var log = new List<string>();
        var completed = new List<ISagaStep>();

        try
        {
            foreach (var step in _steps)
            {
                step.Execute(log);
                completed.Add(step);
            }
        }
        catch (InvalidOperationException)
        {
            for (var i = completed.Count - 1; i >= 0; i--)
            {
                completed[i].Compensate(log);
            }
        }

        return log;
    }
}

public static class Checkout
{
    public static IReadOnlyList<string> Run(bool paymentSucceeds)
    {
        var steps = new ISagaStep[]
        {
            new ReserveStockStep(),
            new ChargePaymentStep(paymentSucceeds),
            new ScheduleShippingStep(),
        };

        return new CheckoutOrchestrator(steps).Run();
    }
}
