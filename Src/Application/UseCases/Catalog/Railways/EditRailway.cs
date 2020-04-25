using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.EditRailway;

namespace TreniniDotNet.Application.UseCases.Catalog.Railways
{
    public sealed class EditRailway : ValidatedUseCase<EditRailwayInput, IEditRailwayOutputPort>, IEditRailwayUseCase
    {
        public EditRailway(IEditRailwayOutputPort output)
            : base(new EditRailwayInputValidator(), output)
        {
        }

        protected override Task Handle(EditRailwayInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
