using System;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.Brands.EditBrand
{
    public sealed class EditBrandUseCase : AbstractUseCase<EditBrandInput, EditBrandOutput, IEditBrandOutputPort>
    {
        private readonly BrandsService _brandsService;
        private readonly IUnitOfWork _unitOfWork;

        public EditBrandUseCase(
            IUseCaseInputValidator<EditBrandInput> inputValidator,
            IEditBrandOutputPort outputPort,
            BrandsService brandService,
            IUnitOfWork unitOfWork)
            : base(inputValidator, outputPort)
        {
            _brandsService = brandService ?? throw new ArgumentNullException(nameof(brandService));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override async Task Handle(EditBrandInput input)
        {
            var brand = await _brandsService.GetBrandBySlug(input.BrandSlug);
            if (brand is null)
            {
                OutputPort.BrandNotFound(input.BrandSlug);
                return;
            }

            ModifiedBrandValues values = input.Values;

            var address = values.Address?.ToDomainAddress();
            var websiteUrl = values.WebsiteUrl.ToUriOpt();
            var mailAddress = values.EmailAddress.ToMailAddressOpt();
            var brandKind = EnumHelpers.OptionalValueFor<BrandKind>(values.BrandType);

            var modifiedBrand = brand.With(
                name: values.Name,
                brandKind: brandKind,
                companyName: values.CompanyName,
                groupName: values.GroupName,
                description: values.Description,
                websiteUrl: websiteUrl,
                mailAddress: mailAddress,
                address: address);

            await _brandsService.UpdateBrand(modifiedBrand);

            var _ = await _unitOfWork.SaveAsync();

            StandardOutput(values.Name, brand.Slug);
        }

        private void StandardOutput(string? name, Slug slug)
        {
            Slug result = slug;
            if (!(name is null))
            {
                result = Slug.Of(name);
            }

            OutputPort.Standard(new EditBrandOutput(result));
        }
    }
}
