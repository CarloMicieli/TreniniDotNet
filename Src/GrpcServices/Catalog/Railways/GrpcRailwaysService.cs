using System;
using System.Threading.Tasks;
using Grpc.Core;
using TreniniDotNet.Application.Catalog.Railways.CreateRailway;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Catalog;
using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.GrpcServices.Catalog.Railways
{
    public sealed class GrpcRailwaysService : RailwaysService.RailwaysServiceBase
    {
        public GrpcRailwaysService(
            RailwayService railwayService,
            IUnitOfWork unitOfWork,
            CreateRailwayPresenter presenter)
        {
            Presenter = presenter ??
                        throw new ArgumentNullException(nameof(presenter));
            UseCase = new CreateRailwayUseCase(presenter, railwayService, unitOfWork);
        }

        private CreateRailwayPresenter Presenter { get; }
        private ICreateRailwayUseCase UseCase { get; }

        public override Task<CreateRailwayResponse> CreateRailway(CreateRailwayRequest request, ServerCallContext context)
        {
            return base.CreateRailway(request, context);
        }
    }
}
