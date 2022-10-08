namespace RetryPattern;

public interface IOperationAsync<T>
{
    public Task<T> Run();
}