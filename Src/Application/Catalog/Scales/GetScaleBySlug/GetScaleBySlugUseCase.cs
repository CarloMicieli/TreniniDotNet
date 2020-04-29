using System.Threading.Tasks;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Application.Catalog.Scales.GetScaleBySlug
{
    public class GetScaleBySlugUseCase : ValidatedUseCase<GetScaleBySlugInput, IGetScaleBySlugOutputPort>, IGetScaleBySlugUseCase
    {
        private readonly ScaleService _scaleService;

        public GetScaleBySlugUseCase(IGetScaleBySlugOutputPort output, ScaleService scaleService)
            : base(new GetScaleBySlugInputValidator(), output)
        {
            this._scaleService = scaleService;
        }

        protected override async Task Handle(GetScaleBySlugInput input)
        {
            var scale = await _scaleService.GetBySlugAsync(input.Slug);
            if (scale is null)
            {
                OutputPort.ScaleNotFound($"The '{input.Slug}' scale was not found");
                return;
            }

            OutputPort.Standard(new GetScaleBySlugOutput(scale));
        }
    }
}