using System.Threading.Tasks;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Domain.Catalog.Brands;

namespace TreniniDotNet.Application.Catalog.Brands.GetBrandBySlug
{
    public class GetBrandBySlugUseCase : ValidatedUseCase<GetBrandBySlugInput, IGetBrandBySlugOutputPort>, IGetBrandBySlugUseCase
    {
        private readonly BrandService _brandService;

        public GetBrandBySlugUseCase(IGetBrandBySlugOutputPort outputPort, BrandService brandService)
            : base(new GetBrandBySlugInputValidator(), outputPort)
        {
            _brandService = brandService;
        }

        protected override async Task Handle(GetBrandBySlugInput input)
        {
            var brand = await _brandService.GetBrandBySlug(input.Slug);
            if (brand is null)
            {
                OutputPort.BrandNotFound($"Brand '{input.Slug}' not found");
                return;
            }

            OutputPort.Standard(new GetBrandBySlugOutput(brand));
        }
    }
}
