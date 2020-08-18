using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using FluentValidation;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs.Validation;

namespace TreniniDotNet.Common.UseCases.Boundaries.Inputs
{
    public class AbstractUseCaseValidator<TUseCaseInput> : AbstractValidator<TUseCaseInput>, IUseCaseInputValidator<TUseCaseInput>
        where TUseCaseInput : IUseCaseInput
    {
        public IReadOnlyList<ValidationError> ValidateInput(TUseCaseInput input)
        {
            var failures = Validate(input);
            return failures.Errors
                .Select(it => new ValidationError(it.PropertyName, it.ErrorMessage))
                .ToImmutableList();
        }
    }
}
