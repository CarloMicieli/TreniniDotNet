using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TreniniDotNet.Application.Boundaries.Catalog.CreateScale;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateScale
{
    public sealed class CreateScaleHandler : AsyncRequestHandler<CreateScaleRequest>
    {
        private readonly ICreateScaleUseCase _useCase;
        private readonly IMapper _mapper;

        public CreateScaleHandler(ICreateScaleUseCase useCase, IMapper mapper)
        {
            _useCase = useCase ??
                throw new ArgumentNullException(nameof(useCase));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        protected override Task Handle(CreateScaleRequest request, CancellationToken cancellationToken)
        {
            var input = _mapper.Map<CreateScaleInput>(request);
            return _useCase.Execute(input);
        }
    }
}