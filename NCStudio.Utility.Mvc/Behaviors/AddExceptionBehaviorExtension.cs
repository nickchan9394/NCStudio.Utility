using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace NCStudio.Utility.Mvc.Behaviors
{
    public static class AddExceptionBehaviorExtension
    {
        public static void AddNCExceptionHandleBehavior(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionBehavior<,>));
        }
    }
}
