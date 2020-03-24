using FluentValidation.Results;
using System;
using System.Collections.Generic;
using TreniniDotNet.Common;

namespace TreniniDotNet.Application.Boundaries
{
    public interface IOutputPortErrors
    {
        void InvalidRequest(IList<ValidationFailure> failures);

        void Error(string? message);

        void Errors(IEnumerable<Error> errors) => Console.WriteLine("");
    }
}
