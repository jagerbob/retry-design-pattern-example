namespace AwfulAPI;

public static class ConfigureAwfulRoutes
{
    public static WebApplication ConfigureRoutes(this WebApplication app)
    {
        var timeoutRequestCount = 0;

        app.MapGet("/AwfullAPI/Timeout", async () =>
        {
            await Task.Delay(5000 - timeoutRequestCount * 1000);
            timeoutRequestCount++;

            return "Hi ! I'm the Awfull API";
        });

        app.MapGet("/AwfullAPI/BadRequest", () => Results.BadRequest());

        app.MapGet("/AwfullAPI/InternalError", () => Results.Problem());

        return app;
    }
}
