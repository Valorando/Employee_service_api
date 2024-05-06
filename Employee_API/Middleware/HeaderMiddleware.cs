namespace Employee_API.Middleware
{
    public class HeaderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HeaderMiddleware> _logger;

        public HeaderMiddleware(RequestDelegate next, ILogger<HeaderMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("X-Special-Header", out var headerValue))
            {
                if (headerValue == "97591")
                {
                    _logger.LogInformation("X-Special-Header корректен, доступ разрешен.");
                    await _next(context);
                }
                else
                {
                
                    _logger.LogWarning($"Значение X-Special-Header некорректно, доступ к {context.Request.Path} не разрешен.");
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("X-Special-Header value is not correct.");
                }
            }
            else
            {
                _logger.LogWarning($"X-Special-Header отсутствует, доступ к {context.Request.Path} не разрешен.");
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("X-Special-Header is not found.");
            }
        }
    }

    public static class HeaderExtensions
    {
        public static IApplicationBuilder UseHeaderValidation(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HeaderMiddleware>();
        }
    }
}
