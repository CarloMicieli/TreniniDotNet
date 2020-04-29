using System.Collections.Generic;
using FluentValidation.Results;

namespace TreniniDotNet.Common.UseCases.Interfaces.Output
{
    public interface IOutputPortErrors
    {
        void InvalidRequest(IList<ValidationFailure> failures);

        void Error(string? message);
    }
}
