using System.Collections.Generic;
using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Application.Boundaries.Catalog.GetRailwaysList
{
    public sealed class GetRailwaysListOutput : IUseCaseOutput
    {
        private readonly List<IRailway> _railways;

        public GetRailwaysListOutput(List<IRailway> railways)
        {
            _railways = railways;
        }

        public List<IRailway> Railways => _railways;
    }
}
