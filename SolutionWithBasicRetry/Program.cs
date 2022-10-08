using Common.Http;
using Common.Retry;

HttpClient client = HttpClientBuilder.Create().WithTimeout(3000);
BasicRetryHandler<HttpResponseMessage> handler = new BasicRetryHandler<HttpResponseMessage>(5, 1000);

try
{
    // Sending a request to a route that will timeout, but with retry
    var response = await handler.Retry(async () => {
        var response = await client.GetAsync("http://localhost:5071/AwfullAPI/Timeout/");
        response.EnsureAPISuccess();
        return response;
    });

    // It works !! 
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
    // Unfortunately, it failed and we have retried for nothing
    Console.WriteLine(e.Message);
}

Console.Read();