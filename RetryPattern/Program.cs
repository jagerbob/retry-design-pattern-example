using RetryPattern;

Console.WriteLine("Démo Retry Pattern : ");

var operation = new OperationAsync();
var retryHandler = new RetryHandler<int>(operation, 3, 1, exception => exception is TimeoutException);
var result = await retryHandler.Run();
Console.WriteLine(result);
Console.ReadKey();