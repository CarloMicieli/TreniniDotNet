using AutoMapper;
using TreniniDotNet.Application.Catalog.Railways.CreateRailway;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.Railways.CreateRailway
{
    public sealed class CreateRailwayHandler : UseCaseHandler<CreateRailwayUseCase, CreateRailwayRequest, CreateRailwayInput>
    {
        public CreateRailwayHandler(CreateRailwayUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
