using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.GetAllRailways;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public sealed class GetAllRailways : IGetAllRailwaysUseCase
    {
        private readonly IOutputPort _outputPort;
        private readonly IUnitOfWork _unitOfWork;
        private readonly RailwayService _railwayService;

        public GetAllRailways(IOutputPort outputPort, IUnitOfWork unitOfWork, RailwayService railwayService)
        {
            _outputPort = outputPort;
            _unitOfWork = unitOfWork;
            _railwayService = railwayService;
        }

        public Task Execute(GetAllRailwaysInput input)
        {
            throw new System.NotImplementedException("TODO");
        }
    }
}
