using Common.Http;

HttpClient client = HttpClientBuilder.Create().WithTimeout(5000);

try
{
    // Sending a request to a route that will timeout
    var response = await client.GetAsync("http://localhost:5071/AwfullAPI/Timeout");
    response.EnsureAPISuccess();
    Console.WriteLine($"Response from awfull API: {response}");
} catch (Exception e)
{
    Console.WriteLine(e.Message);
}

// No answer, what do we do next ??

Console.Read();