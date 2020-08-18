using System.Collections.Generic;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs.Validation;

namespace TreniniDotNet.Common.UseCases.Boundaries.Inputs
{
    public interface IUseCaseInputValidator<in TUseCaseInput>
        where TUseCaseInput : IUseCaseInput
    {
        IReadOnlyList<ValidationError> ValidateInput(TUseCaseInput input);
    }
}
