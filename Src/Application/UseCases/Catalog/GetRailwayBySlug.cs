using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.GetRailwayBySlug;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public sealed class GetRailwayBySlug : IGetRailwayBySlugUseCase
    {
        private readonly IOutputPort _outputPort;
        private readonly IUnitOfWork _unitOfWork;
        private readonly RailwayService _railwayService;

        public GetRailwayBySlug(IOutputPort outputPort, IUnitOfWork unitOfWork, RailwayService railwayService)
        {
            _outputPort = outputPort;
            _unitOfWork = unitOfWork;
            _railwayService = railwayService;
        }

        public Task Execute(GetRailwayBySlugInput input)
        {
            throw new System.NotImplementedException("TODO");
        }
    }
}
