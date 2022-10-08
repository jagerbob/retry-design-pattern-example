using Common.Retry;

namespace Common.Retry;

public class BasicRetryHandler<T>
{
    private int AttemptsCount { get; set; }
    private int MaxAttempts { get; }
    private int Delay { get; }

    public BasicRetryHandler(int maxAttempts = 1, int delay = 1000)
    {
        MaxAttempts = maxAttempts;
        Delay = delay;
    }

    public async Task<T> Retry(Func<Task<T>> operation)
    {
        AttemptsCount = 0;

        while (true)
        {
            try
            {
                AttemptsCount++;
                Console.WriteLine($"[BasicRetryHandler] Attempt {AttemptsCount}");
                var result = await operation();
                Console.WriteLine($"[BasicRetryHandler] Attempt {AttemptsCount} Success");
                return result;
            }
            catch (Exception)
            {
                Console.WriteLine($"[BasicRetryHandler] Attempt {AttemptsCount} failed");
                if (AttemptsCount < MaxAttempts)
                {
                    await Task.Delay(Delay);
                    continue;
                }
                throw;
            }
        }
    }
}