namespace Portfolio.Api.Middleware;

public class ETagMiddleware
{
    private readonly RequestDelegate _next;

    public ETagMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        if (
            context.Response.StatusCode == 200
            && context.Response.ContentType != null
            && context.Response.ContentType.Contains("application/json")
        )
        {
            var body = context.Items["ResponseBody"] as string;
            if (body != null)
            {
                var etag = $"\"{body.GetHashCode()}\"";
                context.Response.Headers["ETag"] = etag;
            }
        }
    }
}
