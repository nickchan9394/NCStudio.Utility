using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace NCStudio.Utility.Mvc.Behaviors
{
    public static class AddValidationBehaviorExtension
    {
        public static void AddNCValidationBehavior(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}
