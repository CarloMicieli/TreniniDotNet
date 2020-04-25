using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.CreateBrand;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Addresses;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Common.Enums;

namespace TreniniDotNet.Application.UseCases.Catalog.Brands
{
    public sealed class CreateBrand : ValidatedUseCase<CreateBrandInput, ICreateBrandOutputPort>, ICreateBrandUseCase
    {
        private readonly BrandService _brandService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBrand(
            ICreateBrandOutputPort outputPort,
            BrandService brandService,
            IUnitOfWork unitOfWork)
            : base(new CreateBrandInputValidator(), outputPort)
        {
            _brandService = brandService ??
                throw new ArgumentNullException(nameof(brandService));

            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override async Task Handle(CreateBrandInput input)
        {
            string brandName = input.Name;
            Slug slug = Slug.Of(brandName);

            bool brandExists = await _brandService.BrandAlreadyExists(slug);
            if (brandExists)
            {
                OutputPort.BrandAlreadyExists($"Brand '{brandName}' already exists");
                return;
            }

            var optAddress = Address.TryCreate(
                input.Address?.Line1,
                input.Address?.Line2,
                input.Address?.City,
                input.Address?.Region,
                input.Address?.PostalCode,
                input.Address?.Country,
                out var address) ? address : null;

            var optUri = input.WebsiteUrl.ToUriOpt();
            var optEmailAddress = input.EmailAddress.ToMailAddressOpt();

            var _ = await _brandService.CreateBrand(
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
