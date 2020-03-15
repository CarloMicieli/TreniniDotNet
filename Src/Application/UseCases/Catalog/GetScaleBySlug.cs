using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.GetScaleBySlug;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public class GetScaleBySlug : ValidatedUseCase<GetScaleBySlugInput, IGetScaleBySlugOutputPort>, IGetScaleBySlugUseCase
    {
        private readonly ScaleService _scaleService;

        public GetScaleBySlug(IGetScaleBySlugOutputPort output, ScaleService scaleService)
            : base(new GetScaleBySlugInputValidator(), output)
        {
            this._scaleService = scaleService;
        }

        protected override async Task Handle(GetScaleBySlugInput input)
        {
            var scale = await _scaleService.GetBy(input.Slug);
            if (scale is null)
            {
                OutputPort.ScaleNotFound($"The '{input.Slug}' scale was not found");
                return;
            }

            OutputPort.Standard(new GetScaleBySlugOutput(scale));
        }
    }
}