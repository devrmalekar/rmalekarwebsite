using MySqlConnector;

public class DatabaseErrorMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<DatabaseErrorMiddleware> _logger;
    private static readonly JsonSerializerOptions _jsonOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };


    public DatabaseErrorMiddleware(RequestDelegate next, ILogger<DatabaseErrorMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (MySqlException ex)
        {
            _logger.LogError(ex, "Database Error: {ErrorMessage}", ex.Message);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            var errorResponse = new { message = "The Database service is currently unavailable. Please try again later." };
            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse, _jsonOptions));
        }
       
    }
}
