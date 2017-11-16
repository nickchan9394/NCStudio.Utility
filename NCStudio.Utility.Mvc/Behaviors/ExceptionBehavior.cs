using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NCStudio.Utility.Mvc.Behaviors
{
    public class ExceptionBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse :class
    {
        public ExceptionBehavior()
        {
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (ValidationException ve)
            {
                return new BadRequestObjectResult(ve.ToString()) as TResponse;
            }
        }
    }
}
