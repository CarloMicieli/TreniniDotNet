using AutoMapper;
using TreniniDotNet.Application.Catalog.CatalogItems.EditRollingStock;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.CatalogItems.EditRollingStock
{
    public sealed class EditRollingStockHandler : UseCaseHandler<IEditRollingStockUseCase, EditRollingStockRequest, EditRollingStockInput>
    {
        public EditRollingStockHandler(IEditRollingStockUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
