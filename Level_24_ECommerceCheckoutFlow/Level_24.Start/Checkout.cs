namespace Level24.Checkout;

// -----------------------------------------------------------------------------
//  CODE MÉTIER À REFACTORISER
// -----------------------------------------------------------------------------
//  La transaction distribuée (réserver le stock, débiter, expédier) est codée
//  d'un bloc, avec la compensation (annulation) écrite à la main dans un `catch`.
//  Les étapes ne sont pas des unités autonomes : impossible d'en ajouter, d'en
//  réordonner, ou de compenser proprement dans l'ordre inverse sans réécrire ce
//  bloc.
//
//  Objectif : confier la transaction à un orchestrateur qui exécute une suite
//  d'étapes autonomes, chacune sachant s'exécuter ET se compenser ; en cas
//  d'échec, l'orchestrateur compense les étapes réussies dans l'ordre inverse.
// -----------------------------------------------------------------------------
public static class Checkout
{
    public static IReadOnlyList<string> Run(bool paymentSucceeds)
    {
        var log = new List<string>();

        log.Add("Stock reserved");

        try
        {
            if (!paymentSucceeds)
            {
                throw new InvalidOperationException("Payment failed.");
            }

            log.Add("Payment charged");
            log.Add("Shipping scheduled");
        }
        catch (InvalidOperationException)
        {
            // Compensation écrite à la main, ici.
            log.Add("Stock released");
        }

        return log;
    }
}
