using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.CreateRailway;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public sealed class CreateRailway : ICreateRailwayUseCase
    {
        private readonly IOutputPort _outputPort;
        private readonly IUnitOfWork _unitOfWork;
        private readonly RailwayService _railwayService;

        public CreateRailway(IOutputPort outputPort, IUnitOfWork unitOfWork, RailwayService railwayService)
        {
            _outputPort = outputPort;
            _unitOfWork = unitOfWork;
            _railwayService = railwayService;
        }

        public Task Execute(CreateRailwayInput input)
        {
            throw new System.NotImplementedException("TODO");
        }
    }
}
