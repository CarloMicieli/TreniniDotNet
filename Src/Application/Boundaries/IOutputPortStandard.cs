using FluentValidation.Results;
using System.Collections.Generic;

namespace TreniniDotNet.Application.Boundaries
{
    public interface IOutputPortStandard<in TUseCaseOutput>
        where TUseCaseOutput : IUseCaseOutput
    {
        void Standard(TUseCaseOutput output);

        void InvalidRequest(List<ValidationFailure> failures);
    }
}
