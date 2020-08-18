using AutoMapper;
using TreniniDotNet.Application.Catalog.CatalogItems.EditRollingStock;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.EditRollingStock
{
    public sealed class EditRollingStockHandler : UseCaseHandler<EditRollingStockUseCase, EditRollingStockRequest, EditRollingStockInput>
    {
        public EditRollingStockHandler(EditRollingStockUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
