using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.GetScaleBySlug;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public class GetScaleBySlug : IUseCase
    {
        private readonly IOutputPort _outputPort;
        private readonly ScaleService _scaleService;

        public GetScaleBySlug(IOutputPort outputPort, ScaleService scaleService)
        {
            _outputPort = outputPort;
            _scaleService = scaleService;
        }

        public Task Execute(GetScaleBySlugInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}