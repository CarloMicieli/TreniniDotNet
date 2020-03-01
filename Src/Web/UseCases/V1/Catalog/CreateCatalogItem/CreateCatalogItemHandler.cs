using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TreniniDotNet.Application.Boundaries.Catalog.CreateCatalogItem;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateCatalogItem
{
    public sealed class CreateCatalogItemHandler : AsyncRequestHandler<CreateCatalogItemRequest>
    {
        private readonly ICreateCatalogItemUseCase _useCase;

        public CreateCatalogItemHandler(ICreateCatalogItemUseCase useCase)
        {
            _useCase = useCase;
        }

        protected override Task Handle(CreateCatalogItemRequest request, CancellationToken cancellationToken)
        {
            return _useCase.Execute(ConvertToInput(request));
        }

        private static CreateCatalogItemInput ConvertToInput(CreateCatalogItemRequest request)
        {
            List<RollingStockInput> rollingStocks = request.RollingStocks
                .Select(rs => new RollingStockInput(rs.Era, rs.Category, rs.Railway, rs.ClassName, rs.RoadNumber, rs.Length))
                .ToList();

            return new CreateCatalogItemInput(
                request.BrandName, 
                request.ItemNumber, 
                request.Description, 
                request.PrototypeDescription,
                request.ModelDescription,
                request.PowerMethod,
                request.Scale,
                rollingStocks);
        }
    }
}

