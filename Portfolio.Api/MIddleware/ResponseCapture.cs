namespace Portfolio.Api.Middleware;

public class ResponseCapture
{
    private readonly RequestDelegate _next;

    public ResponseCapture(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var originalBodyStream = context.Response.Body;
        using var newBodyStream = new MemoryStream();
        context.Response.Body = newBodyStream;

        await _next(context);

        newBodyStream.Seek(0, SeekOrigin.Begin);
        var bodyText = await new StreamReader(newBodyStream).ReadToEndAsync();
        context.Items["ResponseBody"] = bodyText;

        newBodyStream.Seek(0, SeekOrigin.Begin);
        await newBodyStream.CopyToAsync(originalBodyStream);
    }
}
