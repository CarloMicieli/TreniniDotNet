using AutoMapper;
using TreniniDotNet.Application.Boundaries.Catalog.EditRailway;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.EditRailway
{
    public sealed class EditRailwayHandler : UseCaseHandler<IEditRailwayUseCase, EditRailwayRequest, EditRailwayInput>
    {
        public EditRailwayHandler(IEditRailwayUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
