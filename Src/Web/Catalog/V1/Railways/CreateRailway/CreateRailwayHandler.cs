using AutoMapper;
using TreniniDotNet.Application.Catalog.Railways.CreateRailway;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.Railways.CreateRailway
{
    public sealed class CreateRailwayHandler : UseCaseHandler<ICreateRailwayUseCase, CreateRailwayRequest, CreateRailwayInput>
    {
        public CreateRailwayHandler(ICreateRailwayUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
