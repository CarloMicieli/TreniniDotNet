using AutoMapper;
using TreniniDotNet.Application.Boundaries.Catalog.CreateScale;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateScale
{
    public sealed class CreateScaleHandler : UseCaseHandler<ICreateScaleUseCase, CreateScaleRequest, CreateScaleInput>
    {
        public CreateScaleHandler(ICreateScaleUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}