using API.Helpers;
using Microsoft.AspNetCore.Http;
using System.Data.SqlTypes;

namespace API.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("entryuser", out var entryUserHeader))
            {
                entryUserHeader = "Leonid";
                var log = new Log(entryUserHeader.ToString());//Write to Log develpoer UserName
                log.WriteToLog("API request received.");
            }
            await _next(context);
        }
    }
}
