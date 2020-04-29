using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Domain.Catalog.Brands;

namespace TreniniDotNet.Application.Catalog.Brands.EditBrand
{
    public sealed class EditBrandUseCase : ValidatedUseCase<EditBrandInput, IEditBrandOutputPort>, IEditBrandUseCase
    {
        private readonly BrandService _brandService;
        private readonly IUnitOfWork _unitOfWork;

        public EditBrandUseCase(
            IEditBrandOutputPort output,
            BrandService brandService,
            IUnitOfWork unitOfWork)
            : base(new EditBrandInputValidator(), output)
        {
            _brandService = brandService ??
                throw new ArgumentNullException(nameof(brandService));

            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override async Task Handle(EditBrandInput input)
        {
            var brand = await _brandService.GetBrandBySlug(input.BrandSlug);
            if (brand is null)
            {
                OutputPort.BrandNotFound(input.BrandSlug);
                return;
            }

            ModifiedBrandValues values = input.Values;

            var optAddress = values.Address?.ToDomainAddress();
            var optUri = values.WebsiteUrl.ToUriOpt();
            var optEmailAddress = values.EmailAddress.ToMailAddressOpt();
            var optBrandType = EnumHelpers.OptionalValueFor<BrandKind>(values.BrandType);

            await _brandService.UpdateBrand(brand,
                values.Name,
                optBrandType,
                values.CompanyName,
                values.GroupName,
                values.Description,
                optUri,
                optEmailAddress,
                optAddress);

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
