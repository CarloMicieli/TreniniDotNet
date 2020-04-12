using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TreniniDotNet.Application.Boundaries.Catalog.GetCatalogItemBySlug;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetCatalogItemBySlug
{
    public sealed class GetCatalogItemBySlugHandler : AsyncRequestHandler<GetCatalogItemBySlugRequest>
    {
        private readonly IGetCatalogItemBySlugUseCase _useCase;

        public GetCatalogItemBySlugHandler(IGetCatalogItemBySlugUseCase useCase)
        {
            _useCase = useCase ??
                throw new ArgumentNullException(nameof(useCase));
        }

        protected override Task Handle(GetCatalogItemBySlugRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}