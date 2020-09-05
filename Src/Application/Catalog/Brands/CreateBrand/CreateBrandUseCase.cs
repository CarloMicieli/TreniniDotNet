using System;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.Brands.CreateBrand
{
    public sealed class CreateBrandUseCase : AbstractUseCase<CreateBrandInput, CreateBrandOutput, ICreateBrandOutputPort>
    {
        private readonly BrandsService _brandsService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBrandUseCase(
            IUseCaseInputValidator<CreateBrandInput> inputValidator,
            ICreateBrandOutputPort outputPort,
            BrandsService brandService,
            IUnitOfWork unitOfWork)
            : base(inputValidator, outputPort)
        {
            _brandsService = brandService ?? throw new ArgumentNullException(nameof(brandService));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override async Task Handle(CreateBrandInput input)
        {
            string brandName = input.Name;
            var slug = Slug.Of(brandName);

            var brandExists = await _brandsService.BrandAlreadyExists(slug);
            if (brandExists)
            {
                OutputPort.BrandAlreadyExists(slug);
                return;
            }

            var optAddress = input.Address?.ToDomainAddress();
            var optUri = input.WebsiteUrl.ToUriOpt();
            var optEmailAddress = input.EmailAddress.ToMailAddressOpt();

            var _ = await _brandsService.CreateBrand(
                brandName,
                input.CompanyName,
                input.GroupName,
                input.Description,
                optUri,
                optEmailAddress,
                EnumHelpers.OptionalValueFor<BrandKind>(input.Kind) ?? BrandKind.Industrial,
                optAddress);

            await _unitOfWork.SaveAsync();

            BuildOutput(slug);
        }

        private void BuildOutput(Slug slug)
        {
            var output = new CreateBrandOutput(slug);
            OutputPort.Standard(output);
        }
    }
}
