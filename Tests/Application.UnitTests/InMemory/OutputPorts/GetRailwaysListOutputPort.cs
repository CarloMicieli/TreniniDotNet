using FluentValidation.Results;
using System.Collections.Generic;
using TreniniDotNet.Application.Boundaries.Catalog.GetRailwaysList;

namespace TreniniDotNet.Application.InMemory.OutputPorts
{
    public sealed class GetRailwaysListOutputPort : IGetRailwaysListOutputPort
    {
        public string ErrorMessage { set; get; } = null;
        public IList<ValidationFailure> ValidationFailures { set; get; } = null;
        public GetRailwaysListOutput Output { set; get; } = null;

        public void Error(string message)
        {
            this.ErrorMessage = message;
        }

        public void InvalidRequest(IList<ValidationFailure> failures)
        {
            ValidationFailures = failures;
        }

        public void Standard(GetRailwaysListOutput output)
        {
            Output = output;
        }
    }
}
