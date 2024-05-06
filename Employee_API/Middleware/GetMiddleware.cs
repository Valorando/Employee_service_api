namespace Employee_API.Middleware
{
    public class GetMiddleware
    {
        private readonly RequestDelegate _next;

        public GetMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method == "GET")
            {
                Console.WriteLine($"{context.Request.Method} запрос по пути: {context.Request.Path} начал обрабатываться");
                await _next(context);
                Console.WriteLine($"{context.Request.Method} запрос по пути: {context.Request.Path} обработан");
            }
            else
            {
                await _next(context);
            }
        }
    }

    public static class GetMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GetMiddleware>();
        }
    }
}
