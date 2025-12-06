using System.Diagnostics;

namespace BlogApp.Middlewares
{
    public class RequestTimingMiddleware
    {
        //Middleware is useful to .Net core apps request process. And creates responses.
        //It is a software tool. It is used for http request and responses, also vairous processes are maden thanks to this tool. It completes the first request and then it moves to another.

        //Let's entegrate it to program.cs

        private readonly RequestDelegate _next;
        public RequestTimingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            //Start timing
            var watch = Stopwatch.StartNew();
            await _next(context);

            watch.Stop();
            //take the time 
            var elapsed = watch.ElapsedMilliseconds;
            //write it to debug 
            Debug.WriteLine($"Request path: {context.Request.Path} -- Process timeline: {elapsed} ms");

        }
    }
}

    

