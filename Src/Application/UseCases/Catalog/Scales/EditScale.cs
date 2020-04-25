using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.EditScale;

namespace TreniniDotNet.Application.UseCases.Catalog.Scales
{
    public sealed class EditScale : ValidatedUseCase<EditScaleInput, IEditScaleOutputPort>, IEditScaleUseCase
    {
        public EditScale(IEditScaleOutputPort output)
            : base(new EditScaleInputValidator(), output)
        {
        }

        protected override Task Handle(EditScaleInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
