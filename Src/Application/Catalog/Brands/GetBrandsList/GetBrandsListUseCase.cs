using System;
using System.Threading.Tasks;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Catalog.Brands;

namespace TreniniDotNet.Application.Catalog.Brands.GetBrandsList
{
    public sealed class GetBrandsListUseCase : AbstractUseCase<GetBrandsListInput, GetBrandsListOutput, IGetBrandsListOutputPort>
    {
        private readonly BrandsService _brandsService;

        public GetBrandsListUseCase(IUseCaseInputValidator<GetBrandsListInput> inputValidator, IGetBrandsListOutputPort outputPort, BrandsService brandsService)
            : base(inputValidator, outputPort)
        {
            _brandsService = brandsService ??
                             throw new ArgumentNullException(nameof(brandsService));
        }

        protected override async Task Handle(GetBrandsListInput input)
        {
            var paginatedResult = await _brandsService.FindAllBrands(input.Page);
            OutputPort.Standard(new GetBrandsListOutput(paginatedResult));
        }
    }
}
