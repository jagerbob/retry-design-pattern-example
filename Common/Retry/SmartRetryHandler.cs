using Common.Retry;

namespace Common.Retry;

public class SmartRetryHandler<T>
{
    private int AttemptsCount { get; set; }
    private int MaxAttempts { get; }
    private int Delay { get; }
    private Predicate<Exception> ShouldRetry { get; }

    public SmartRetryHandler(Predicate<Exception> shouldRetry, int maxAttempts = 1, int delay = 1000)
    {
        MaxAttempts = maxAttempts;
        Delay = delay;
        ShouldRetry = shouldRetry;
    }

    public async Task<T> Retry(Func<Task<T>> operation)
    {
        AttemptsCount = 0;

        while (true)
        {
            try
            {
                AttemptsCount++;
                Console.WriteLine($"[SmartRetryHandler] Attempt {AttemptsCount}");
                var result = await operation();
                Console.WriteLine($"[SmartRetryHandler] Attempt {AttemptsCount} Success");
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine($"[SmartRetryHandler] Attempt {AttemptsCount} failed");
                if (ShouldRetry(e) && AttemptsCount < MaxAttempts)
                {
                    await Task.Delay(Delay);
                    continue;
                }
                throw;
            }
        }
    }
}