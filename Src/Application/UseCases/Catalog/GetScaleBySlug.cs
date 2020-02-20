using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.GetScaleBySlug;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public class GetScaleBySlug : IGetScaleBySlugUseCase
    {
        private readonly IOutputPort _outputPort;
        private readonly IUnitOfWork unitOfWork;
        private readonly ScaleService _scaleService;

        public GetScaleBySlug(IOutputPort outputPort, IUnitOfWork unitOfWork, ScaleService scaleService)
        {
            _outputPort = outputPort;
            this.unitOfWork = unitOfWork;
            _scaleService = scaleService;
        }

        public Task Execute(GetScaleBySlugInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}