namespace RetryPattern;

public class OperationAsync : IOperationAsync<int>
{
    private static int _nbAttemps;
    public async Task<int> Run()
    {
        _nbAttemps++;
        await Task.Delay(1000);
        if (_nbAttemps <= 2)
        {
            throw new TimeoutException("Request timeout");
        }
        return 1;
    }
}