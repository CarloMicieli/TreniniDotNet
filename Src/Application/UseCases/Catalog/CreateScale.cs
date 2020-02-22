using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.CreateScale;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public class CreateScale : ICreateScaleUseCase
    {
        private readonly IOutputPort _outputPort;
        private readonly ScaleService _scaleService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateScale(IOutputPort outputPort, IUnitOfWork unitOfWork, ScaleService scaleService)
        {
            _outputPort = outputPort;
            _scaleService = scaleService;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(CreateScaleInput input)
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

        public void BuildOutput(IScale scale)
        {
            var output = new CreateScaleOutput(scale);
            _outputPort.Standard(output);
        }
    }
}