using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Domain.Catalog.CatalogItems;

namespace TreniniDotNet.Application.Catalog.CatalogItems.EditRollingStock
{
    public sealed class EditRollingStockUseCase : ValidatedUseCase<EditRollingStockInput, IEditRollingStockOutputPort>, IEditRollingStockUseCase
    {
        private readonly CatalogItemService _catalogItemService;
        private readonly IUnitOfWork _unitOfWork;

        public EditRollingStockUseCase(IEditRollingStockOutputPort output,
            CatalogItemService catalogItemService,
            IUnitOfWork unitOfWork)
            : base(new EditRollingStockInputValidator(), output)
        {
            _catalogItemService = catalogItemService ??
                throw new ArgumentNullException(nameof(catalogItemService));
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override Task Handle(EditRollingStockInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
