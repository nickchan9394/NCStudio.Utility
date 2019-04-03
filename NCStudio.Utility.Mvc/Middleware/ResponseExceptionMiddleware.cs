using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace NCStudio.Utility.Mvc.Middleware
{
    public class ResponseExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(e.ToString());
                return;
            }
        }
    }
}
