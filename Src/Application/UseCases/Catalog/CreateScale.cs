using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.CreateScale;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Domain.Catalog.Scales;

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
            bool scaleExists = await _scaleService.ScaleAlreadyExists(input.Scale.Name);
            if (scaleExists)
            {
                _outputPort.ScaleAlreadyExists($"Scale '{input.Scale.Name}' already exists");
                return;
            }

            Scale scale = await _scaleService.CreateScale(input.Scale);

            await _unitOfWork.SaveAsync();

            BuildOutput(scale);    
        }

        public void BuildOutput(Scale scale)
        {
            var output = new CreateScaleOutput(scale);
            _outputPort.Standard(output);
        }
    }
}