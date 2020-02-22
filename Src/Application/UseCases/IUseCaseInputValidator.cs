using FluentValidation.Results;
using System.Collections.Generic;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.UseCases
{
    public interface IUseCaseInputValidator<TInput>
        where TInput : IUseCaseInput
    {
        IList<ValidationFailure> Validate(TInput input);
    }
}
