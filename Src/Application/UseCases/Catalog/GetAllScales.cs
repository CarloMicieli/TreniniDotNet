using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.GetAllScales;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public sealed class GetAllScales : IGetAllScalesUseCase
    {
        private readonly ScaleService _scaleService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOutputPort _outputPort;

        public GetAllScales(ScaleService scaleService, IUnitOfWork unitOfWork, IOutputPort outputPort)
        {
            _scaleService = scaleService;
            _unitOfWork = unitOfWork;
            _outputPort = outputPort;
        }

        public Task Execute(GetAllScalesInput input)
        {
            throw new System.NotImplementedException("TODO");
        }
    }
}
