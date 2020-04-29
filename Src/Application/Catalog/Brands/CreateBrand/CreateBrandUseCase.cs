using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Domain.Catalog.Brands;

namespace TreniniDotNet.Application.Catalog.Brands.CreateBrand
{
    public sealed class CreateBrandUseCase : ValidatedUseCase<CreateBrandInput, ICreateBrandOutputPort>, ICreateBrandUseCase
    {
        private readonly BrandService _brandService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBrandUseCase(
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

            var optAddress = input.Address?.ToDomainAddress();
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
