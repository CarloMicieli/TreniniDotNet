using System.Collections.Generic;
using FluentValidation.Results;
using TreniniDotNet.Common.UseCases.Interfaces.Input;

namespace TreniniDotNet.Common.UseCases
{
    public interface IUseCaseInputValidator<in TInput>
        where TInput : IUseCaseInput
    {
        IList<ValidationFailure> Validate(TInput input);
    }
}
