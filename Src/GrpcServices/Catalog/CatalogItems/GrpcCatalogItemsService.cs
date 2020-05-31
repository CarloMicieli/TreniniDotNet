using System;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using TreniniDotNet.Application.Catalog.CatalogItems;
using TreniniDotNet.Application.Catalog.CatalogItems.CreateCatalogItem;
using TreniniDotNet.Catalog;
using TreniniDotNet.GrpcServices.Extensions;

namespace TreniniDotNet.GrpcServices.Catalog.CatalogItems
{
    public sealed class GrpcCatalogItemsService : CatalogItemsService.CatalogItemsServiceBase
    {
        public GrpcCatalogItemsService(
            ICreateCatalogItemUseCase useCase,
            CreateCatalogItemPresenter presenter,
            ILogger<GrpcCatalogItemsService> log)
        {
            Presenter = presenter ??
                throw new ArgumentNullException(nameof(presenter));
            UseCase = useCase ??
                throw new ArgumentNullException(nameof(useCase));
            Log = log ??
                throw new ArgumentNullException(nameof(log));
        }

        private ILogger<GrpcCatalogItemsService> Log { get; }
        private CreateCatalogItemPresenter Presenter { get; }
        private ICreateCatalogItemUseCase UseCase { get; }

        public override async Task<CreateCatalogItemResponse> CreateCatalogItem(CreateCatalogItemRequest request, ServerCallContext context)
        {
            await UseCase.Execute(InputFromRequest(request));
            return Presenter.Response;
        }

        public override async Task<CreateCatalogItemsResponse> CreateCatalogItems(IAsyncStreamReader<CreateCatalogItemRequest> requestStream, ServerCallContext context)
        {
            var created = 0;

            while (await requestStream.MoveNext())
            {
                var input = InputFromRequest(requestStream.Current);

                try
                {
                    await UseCase.Execute(input);
                    Log.LogInformation("Catalog item {0} {1} has been created (slug: {2})", input.Brand, input.ItemNumber, Presenter.Response.Slug);
                    created++;
                }
                catch (RpcException rpcEx)
                {
                    Log.LogError("Catalog item {0} {1}, error: {2}", input.Brand, input.ItemNumber, rpcEx.Message);
                }
            }

            return new CreateCatalogItemsResponse { Created = created };
        }

        private static CreateCatalogItemInput InputFromRequest(CreateCatalogItemRequest request)
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

            return new CreateCatalogItemInput(
                request.Brand,
                request.ItemNumber,
                request.Description,
                request.PrototypeDescription.ToNullableString(),
                request.ModelDescription.ToNullableString(),
                request.PowerMethod.ToPowerMethodName() ?? "",
                request.Scale,
                request.DeliveryDate.ToNullableString(),
                request.IsAvailable,
                rollingStocks);
        }

        private static LengthOverBufferInput? FromRollingStockLength(RollingStockLength rpcLength)
        {
            decimal? millimeters = null;
            decimal? inches = null;

            if (rpcLength.Millimeters.HasValue)
            {
                millimeters = (decimal) rpcLength.Millimeters;
            }

            if (rpcLength.Inches.HasValue)
            {
                inches = (decimal) rpcLength.Inches;
            }

            return new LengthOverBufferInput(millimeters, inches);
        }
    }
}
