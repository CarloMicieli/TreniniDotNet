using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateCatalogItem
{
    public sealed class CreateCatalogItemHandler : AsyncRequestHandler<CreateCatalogItemRequest>
    {
        protected override Task Handle(CreateCatalogItemRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}