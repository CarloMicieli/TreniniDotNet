using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using TreniniDotNet.Common.UseCases.Interfaces.Input;

namespace TreniniDotNet.Common.UseCases
{
    public sealed class UseCaseInputValidator<TInput> : IUseCaseInputValidator<TInput>
        where TInput : IUseCaseInput
    {
        private readonly IEnumerable<IValidator<TInput>> _validators;

        public UseCaseInputValidator(IValidator<TInput> validator)
        {
            if (validator is null)
            {
                throw new ArgumentNullException(nameof(validator));
            }

            _validators = new List<IValidator<TInput>>() { validator };
        }

        public IList<ValidationFailure> Validate(TInput input)
        {
            var failures = _validators
                .Select(validator => validator.Validate(input))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            return failures;
        }
    }
}
