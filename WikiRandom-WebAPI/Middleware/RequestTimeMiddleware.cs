using System.Diagnostics;

namespace WikiRandom_WebAPI.Middleware
{
    public class RequestTimeMiddleware : IMiddleware
    {
        private readonly ILogger logger;
        private Stopwatch stopwatch;

        public RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger)
        {
            this.logger = logger;
            stopwatch = new Stopwatch();
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            stopwatch.Start();
            await next.Invoke(context);
            stopwatch.Stop();
            if (stopwatch.ElapsedMilliseconds > 30000)
            {
                logger.LogInformation("Request timeout. Request took more than 30 seconds.");
            }
        }
    }
}
