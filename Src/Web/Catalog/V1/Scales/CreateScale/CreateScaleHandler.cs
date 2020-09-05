using AutoMapper;
using TreniniDotNet.Application.Catalog.Scales.CreateScale;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.Scales.CreateScale
{
    public sealed class CreateScaleHandler : UseCaseHandler<CreateScaleUseCase, CreateScaleRequest, CreateScaleInput>
    {
        public CreateScaleHandler(CreateScaleUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}