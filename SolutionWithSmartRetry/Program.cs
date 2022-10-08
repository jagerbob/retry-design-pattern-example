using Common.Exceptions;
using Common.Http;
using Common.Retry;

HttpClient client = HttpClientBuilder.Create().WithTimeout(3000);
Predicate<Exception> shouldRetry = (e) => (e is APIResponseException apiException && apiException.StatusCode == 500) || e is TaskCanceledException;
SmartRetryHandler<HttpResponseMessage> handler = new SmartRetryHandler<HttpResponseMessage>(shouldRetry, 5, 1000);

try
{
    // Sending a request to a route that will timeout, but with retry
    var response = await handler.Retry(async () => {
        var response = await client.GetAsync("http://localhost:5071/AwfullAPI/Timeout/");
        response.EnsureAPISuccess();
        return response;
    });
    Console.WriteLine($"Response from awfull API: {await response.Content.ReadAsStringAsync()}");
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

try
{
    // Sending a request to a route that will respond with 403 error code
    var response = await handler.Retry(async () => {
        var response = await client.GetAsync("http://localhost:5071/AwfullAPI/BadRequest/");
        response.EnsureAPISuccess();
        return response;
    });
    Console.WriteLine($"Response from awfull API: {await response.Content.ReadAsStringAsync()}");
}
catch (Exception e)
{
    // As expected, no valid response received, but we have unnecessarely retried
    Console.WriteLine(e.Message);
}

try
{
    // Sending a request to a route that will respond with 403 error code
    var response = await handler.Retry(async () => {
        var response = await client.GetAsync("http://localhost:5071/AwfullAPI/InternalError/");
        response.EnsureAPISuccess();
        return response;
    });
    Console.WriteLine($"Response from awfull API: {await response.Content.ReadAsStringAsync()}");
}
catch (Exception e)
{
    // As expected, no valid response received, but we have unnecessarely retried
    Console.WriteLine(e.Message);
}

Console.Read();