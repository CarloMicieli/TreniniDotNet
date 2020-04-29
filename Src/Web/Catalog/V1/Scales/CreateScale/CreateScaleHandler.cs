using AutoMapper;
using TreniniDotNet.Application.Catalog.Scales.CreateScale;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.Scales.CreateScale
{
    public sealed class CreateScaleHandler : UseCaseHandler<ICreateScaleUseCase, CreateScaleRequest, CreateScaleInput>
    {
        public CreateScaleHandler(ICreateScaleUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}