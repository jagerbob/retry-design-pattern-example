namespace Common.Http;

public class HttpClientBuilder
{
    private readonly HttpClient _httpClient;

    public HttpClientBuilder()
    {
        _httpClient = new HttpClient();
    }

    public static HttpClientBuilder Create() => new HttpClientBuilder();

    public HttpClientBuilder WithTimeout(int timeout)
    {
        _httpClient.Timeout = TimeSpan.FromMilliseconds(timeout);
        return this;
    }

    public HttpClient Build() => _httpClient;

    public static implicit operator HttpClient(HttpClientBuilder b) => b.Build();

}
