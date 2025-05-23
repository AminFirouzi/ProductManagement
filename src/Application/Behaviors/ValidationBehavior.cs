﻿using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
       where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var validationFailures = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(request, cancellationToken)));

            var failures = validationFailures
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .ToList();

            if (failures.Any())
            {
                throw new FluentValidation.ValidationException(failures);
            }
            return await next();
        }
    }
}
