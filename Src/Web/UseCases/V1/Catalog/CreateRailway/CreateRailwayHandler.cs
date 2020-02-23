using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.CreateRailway;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateRailway
{
    public sealed class CreateRailwayHandler : AsyncRequestHandler<CreateRailwayRequest>
    {
        private readonly ICreateRailwayUseCase _useCase;

        public CreateRailwayHandler(ICreateRailwayUseCase useCase)
        {
            _useCase = useCase ??
                throw new ArgumentNullException(nameof(useCase));
        }

        protected override Task Handle(CreateRailwayRequest request, CancellationToken cancellationToken)
        {
            var input = new CreateRailwayInput(request.Name,
                request.CompanyName,
                request.Country,
                request.Status,
                request.OperatingSince,
                request.OperatingUntil);
            return _useCase.Execute(input);
        }
    }
}
