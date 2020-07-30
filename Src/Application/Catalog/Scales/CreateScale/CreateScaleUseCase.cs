using System;
using System.Collections.Immutable;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.Scales.CreateScale
{
    public sealed class CreateScaleUseCase : AbstractUseCase<CreateScaleInput, CreateScaleOutput, ICreateScaleOutputPort>
    {
        private readonly ScalesService _scaleService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateScaleUseCase(
            IUseCaseInputValidator<CreateScaleInput> inputValidator,
            ICreateScaleOutputPort outputPort,
            ScalesService scalesService,
            IUnitOfWork unitOfWork)
            : base(inputValidator, outputPort)
        {
            _scaleService = scalesService ?? throw new ArgumentNullException(nameof(scalesService));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override async Task Handle(CreateScaleInput input)
        {
            string name = input.Name!;
            var slug = Slug.Of(name);

            bool scaleExists = await _scaleService.ScaleAlreadyExists(slug);
            if (scaleExists)
            {
                OutputPort.ScaleAlreadyExists(slug);
                return;
            }

            var scaleGauge = input.Gauge.ToScaleGauge();

            var _ = await _scaleService.CreateScale(
                name,
                Ratio.Of(input.Ratio ?? 0M),
                scaleGauge,
                input.Description,
                ImmutableHashSet<ScaleStandard>.Empty,
                input.Weight);

            await _unitOfWork.SaveAsync();

            BuildOutput(slug);
        }

        private void BuildOutput(Slug slug)
        {
            var output = new CreateScaleOutput(slug);
            OutputPort.Standard(output);
        }
    }
}
