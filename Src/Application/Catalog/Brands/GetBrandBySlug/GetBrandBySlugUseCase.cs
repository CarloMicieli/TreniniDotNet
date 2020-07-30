using System;
using System.Threading.Tasks;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Catalog.Brands;

namespace TreniniDotNet.Application.Catalog.Brands.GetBrandBySlug
{
    public sealed class GetBrandBySlugUseCase : AbstractUseCase<GetBrandBySlugInput, GetBrandBySlugOutput, IGetBrandBySlugOutputPort>
    {
        private readonly BrandsService _brandsService;

        public GetBrandBySlugUseCase(
            IUseCaseInputValidator<GetBrandBySlugInput> inputValidator,
            IGetBrandBySlugOutputPort outputPort,
            BrandsService brandsService)
            : base(inputValidator, outputPort)
        {
            _brandsService = brandsService ?? throw new ArgumentNullException(nameof(brandsService));
        }

        protected override async Task Handle(GetBrandBySlugInput input)
        {
            var brand = await _brandsService.GetBrandBySlug(input.Slug);
            if (brand is null)
            {
                OutputPort.BrandNotFound($"Brand '{input.Slug}' not found");
                return;
            }

            OutputPort.Standard(new GetBrandBySlugOutput(brand));
        }
    }
}
