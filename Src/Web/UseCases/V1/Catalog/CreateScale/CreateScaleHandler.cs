using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TreniniDotNet.Application.Boundaries.CreateScale;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateScale
{
    public class CreateScaleHandler : AsyncRequestHandler<CreateScaleRequest>
    {
        private readonly IUseCase _useCase;
        private readonly IScalesFactory _scaleFactory;

        public CreateScaleHandler(IUseCase useCase, IScalesFactory scaleFactory)
        {
            _useCase = useCase;
            _scaleFactory = scaleFactory;
        }

        protected override Task Handle(CreateScaleRequest request, CancellationToken cancellationToken)
        {
            var input = new CreateScaleInput(_scaleFactory.NewScale(request.Name, request.Ratio.Value, request.Gauge.Value, TrackGauge.Standard));
            return _useCase.Execute(input);
        }
    }
}