using System;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using TreniniDotNet.Application.Catalog.CatalogItems;
using TreniniDotNet.Application.Catalog.CatalogItems.CreateCatalogItem;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Catalog;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.GrpcServices.Extensions;

namespace TreniniDotNet.GrpcServices.Catalog.CatalogItems
{
    public sealed class GrpcCatalogItemsService : CatalogItemsService.CatalogItemsServiceBase
    {
        public GrpcCatalogItemsService(
            CatalogItemService catalogItemService,
            IRollingStocksFactory rollingStocksFactory,
            IUnitOfWork unitOfWork,
            CreateCatalogItemPresenter presenter)
        {
            Presenter = presenter ??
                        throw new ArgumentNullException(nameof(presenter));
            UseCase = new CreateCatalogItemUseCase(presenter, catalogItemService, rollingStocksFactory, unitOfWork);
        }

        private CreateCatalogItemPresenter Presenter { get; }
        private ICreateCatalogItemUseCase UseCase { get; }

        public override async Task<CreateCatalogItemResponse> CreateCatalogItem(CreateCatalogItemRequest request, ServerCallContext context)
        {
            var rollingStocks = request.RollingStocks
                .Select(it => new RollingStockInput(
                    it.Epoch,
                    it.Category.ToCategoryName() ?? "",
                    it.Railway,
                    it.ClassName.ToNullableString(),
                    it.RoadNumber.ToNullableString(),
                    it.TypeName.ToNullableString(),
                    it.Couplers.ToCouplers(),
                    it.Livery.ToNullableString(),
                    it.PassengerCarType.ToPassengerCarType(),
                    it.ServiceLevel.ToNullableString(),
                    FromRollingStockLength(it.Length),
                    it.Control.ToControlName(),
                    it.DccInterface.ToDccInterfaceName()))
                .ToList();

            await UseCase.Execute(new CreateCatalogItemInput(
                request.Brand,
                request.ItemNumber,
                request.Description,
                request.PrototypeDescription.ToNullableString(),
                request.ModelDescription.ToNullableString(),
                request.PowerMethod.ToPowerMethodName() ?? "",
                request.Scale,
                request.DeliveryDate.ToNullableString(),
                request.IsAvailable,
                rollingStocks));

            return Presenter.Response;
        }

        private static LengthOverBufferInput? FromRollingStockLength(RollingStockLength rpcLength)
        {
            var millimeters = rpcLength.Millimeters <= 0.0f ? (decimal?) null : (decimal) rpcLength.Millimeters;
            var inches = rpcLength.Inches <= 0.0f ? (decimal?) null : (decimal) rpcLength.Inches;
            return new LengthOverBufferInput(millimeters, inches);
        }
    }
}
