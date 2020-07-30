using AutoMapper;
using TreniniDotNet.Application.Catalog.Scales.EditScale;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.Scales.EditScale
{
    public sealed class EditScaleHandler : UseCaseHandler<EditScaleUseCase, EditScaleRequest, EditScaleInput>
    {
        public EditScaleHandler(EditScaleUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
