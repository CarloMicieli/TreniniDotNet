using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.UseCases
{
    public sealed class UseCaseInputValidator<TInput> : IUseCaseInputValidator<TInput>
        where TInput : IUseCaseInput
    {
        private readonly IEnumerable<IValidator<TInput>> _validators;

        public UseCaseInputValidator(IEnumerable<IValidator<TInput>> validators)
        {
            _validators = validators;
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
