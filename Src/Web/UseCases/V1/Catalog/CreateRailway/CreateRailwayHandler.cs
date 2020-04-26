using AutoMapper;
using TreniniDotNet.Application.Boundaries.Catalog.CreateRailway;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateRailway
{
    public sealed class CreateRailwayHandler : UseCaseHandler<ICreateRailwayUseCase, CreateRailwayRequest, CreateRailwayInput>
    {
        public CreateRailwayHandler(ICreateRailwayUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
