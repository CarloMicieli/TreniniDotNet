using FluentValidation.Results;
using System.Collections.Generic;

namespace TreniniDotNet.Application.Boundaries
{
    public interface IOutputPortErrors
    {
        void InvalidRequest(IList<ValidationFailure> failures);

        void Error(string? message);
    }
}
