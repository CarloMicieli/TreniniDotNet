using System;
using System.Threading.Tasks;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Application.Catalog.Scales.GetScaleBySlug
{
    public sealed class GetScaleBySlugUseCase : AbstractUseCase<GetScaleBySlugInput, GetScaleBySlugOutput, IGetScaleBySlugOutputPort>
    {
        private readonly ScalesService _scalesService;

        public GetScaleBySlugUseCase(IUseCaseInputValidator<GetScaleBySlugInput> inputValidator, IGetScaleBySlugOutputPort outputPort, ScalesService scalesService)
            : base(inputValidator, outputPort)
        {
            _scalesService = scalesService ?? throw new ArgumentNullException(nameof(scalesService));
        }

        protected override async Task Handle(GetScaleBySlugInput input)
        {
            var scale = await _scalesService.GetBySlugAsync(input.Slug);
            if (scale is null)
            {
                OutputPort.ScaleNotFound($"The '{input.Slug}' scale was not found");
                return;
            }

            OutputPort.Standard(new GetScaleBySlugOutput(scale));
        }
    }
}
