using AutoMapper;
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
        private readonly IMapper _mapper;

        public CreateRailwayHandler(ICreateRailwayUseCase useCase, IMapper mapper)
        {
            _useCase = useCase ??
                throw new ArgumentNullException(nameof(useCase));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        protected override Task Handle(CreateRailwayRequest request, CancellationToken cancellationToken)
        {
            var input = _mapper.Map<CreateRailwayInput>(request);
            return _useCase.Execute(input);
        }
    }
}
