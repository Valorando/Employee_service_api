namespace Employee_API.Middleware
{
    public class PostMiddleware
    {
        private readonly RequestDelegate _next;

        public PostMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method == "POST")
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

    public static class PostMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PostMiddleware>();
        }
    }
}
