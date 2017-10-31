using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace NCStudio.Utility.Mvc.Middleware
{
    public static class ResponseExceptionMiddlewareExtensions
    {
        /// <summary>
        /// Exception string and 500 status Code will be returned if an exception throws inside the server
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseResponseException(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseExceptionMiddleware>();
        }
    }
}
