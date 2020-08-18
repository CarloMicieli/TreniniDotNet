using System.Collections.Generic;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs.Validation;

namespace TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports
{
    /// <summary>
    /// The <em>output port</em> when use case handling results in an error.
    /// </summary>
    public interface IErrorOutputPort : IOutputPort
    {
        void InvalidRequest(IEnumerable<ValidationError> validationErrors);

        void Error(string errorMessage);
    }
}