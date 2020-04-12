using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TreniniDotNet.Application.Boundaries.Catalog.CreateCatalogItem;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateCatalogItem
{
    public sealed class CreateCatalogItemHandler : AsyncRequestHandler<CreateCatalogItemRequest>
    {
        private readonly ICreateCatalogItemUseCase _useCase;
        private readonly IMapper _mapper;

        public CreateCatalogItemHandler(ICreateCatalogItemUseCase useCase, IMapper mapper)
        {
            _useCase = useCase ??
                throw new ArgumentNullException(nameof(useCase));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        protected override Task Handle(CreateCatalogItemRequest request, CancellationToken cancellationToken)
        {
            var input = _mapper.Map<CreateCatalogItemInput>(request);
            return _useCase.Execute(input);
        }
    }
}

