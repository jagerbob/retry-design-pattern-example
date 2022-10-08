namespace RetryPattern;

public class RetryHandler<T>
{
    private int Attemps { get; set; }
    private int MaxAttemps { get; }
    private int Delay { get; }
    public List<Exception> Errors { get; } = new();
    private IOperationAsync<T> OperationAsync { get; }
    private Predicate<Exception> ShouldRetryPredicate { get; }

    public RetryHandler(
        IOperationAsync<T> operationAsync,
        int maxAttempts,
        int delay,
        Predicate<Exception> shouldRetryPredicate)
    {
        OperationAsync = operationAsync;
        MaxAttemps = maxAttempts;
        Delay = delay;
        ShouldRetryPredicate = shouldRetryPredicate;
    }

    public async Task<T?> Run()
    {
        while (true)
        {
            try
            {
                Attemps++;
                Console.WriteLine($"Tentative #{Attemps} : ");
                var result = await OperationAsync.Run();
                Console.WriteLine("Succès !");
                return result;
            }
            catch (Exception e)
            {
                Errors.Add(e);
                Console.WriteLine($"Erreur survenue : {e.Message}");
                if (Attemps >= MaxAttemps || !ShouldRetryPredicate(e))
                {
                    throw;
                }

                Console.WriteLine($"Délai de {Delay} secondes ...");
                await Task.Delay(Delay * 1000);
            }
        }
    }
}