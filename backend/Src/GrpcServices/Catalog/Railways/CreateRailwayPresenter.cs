using TreniniDotNet.Application.Catalog.Railways.CreateRailway;
using TreniniDotNet.Catalog;
using TreniniDotNet.GrpcServices.Infrastructure;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.GrpcServices.Catalog.Railways
{
    public sealed class CreateRailwayPresenter : DefaultGrpcPresenter<CreateRailwayOutput, CreateRailwayResponse>, ICreateRailwayOutputPort
    {
        public CreateRailwayPresenter()
            : base(Mapping)
        {
        }

        private static CreateRailwayResponse Mapping(CreateRailwayOutput output) =>
            new CreateRailwayResponse { Slug = output.Slug };

        public void RailwayAlreadyExists(Slug railway) =>
            AlreadyExists(railway.ToString());
    }
}
