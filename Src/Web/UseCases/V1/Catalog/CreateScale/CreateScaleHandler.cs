using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TreniniDotNet.Application.Boundaries.Catalog.CreateScale;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateScale
{
    public sealed class CreateScaleHandler : AsyncRequestHandler<CreateScaleRequest>
    {
        private readonly ICreateScaleUseCase _useCase;

        public CreateScaleHandler(ICreateScaleUseCase useCase)
        {
            _useCase = useCase ??
                throw new ArgumentNullException(nameof(useCase));
        }

        protected override Task Handle(CreateScaleRequest request, CancellationToken cancellationToken)
        {
            var input = new CreateScaleInput(
                request.Name,
                request.Ratio,
                request.Gauge,
                request.TrackGauge,
                request.Notes);
            return _useCase.Execute(input);
        }
    }
}