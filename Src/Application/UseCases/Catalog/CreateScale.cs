using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.CreateScale;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public class CreateScale : ValidatedUseCase<CreateScaleInput, ICreateScaleOutputPort>, ICreateScaleUseCase
    {
        private readonly ICreateScaleOutputPort _outputPort;
        private readonly ScaleService _scaleService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateScale(IUseCaseInputValidator<CreateScaleInput> validator,
            ICreateScaleOutputPort outputPort, 
            ScaleService scaleService, 
            IUnitOfWork unitOfWork)
            : base(validator, outputPort)
        {
            _outputPort = outputPort;
            _scaleService = scaleService;
            _unitOfWork = unitOfWork;
        }

        protected override async Task Handle(CreateScaleInput input)
        {
            bool scaleExists = await _scaleService.ScaleAlreadyExists(input.Name!);
            if (scaleExists)
            {
                _outputPort.ScaleAlreadyExists($"Scale '{input.Name}' already exists");
                return;
            }

            IScale scale = await _scaleService.CreateScale(
                input.Name!,
                Ratio.Of(input.Ratio ?? 0M),
                Gauge.OfMillimiters(input.Gauge ?? 0M),
                input.TrackGauge.ToTrackGauge(),
                input.Notes);

            await _unitOfWork.SaveAsync();

            BuildOutput(scale);
        }

        private void BuildOutput(IScale scale)
        {
            var output = new CreateScaleOutput(scale);
            _outputPort.Standard(output);
        }
    }
}