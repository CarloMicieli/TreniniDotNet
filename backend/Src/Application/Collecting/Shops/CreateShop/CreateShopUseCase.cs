using System;
using System.Net.Mail;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.SharedKernel.Addresses;
using TreniniDotNet.SharedKernel.PhoneNumbers;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Collecting.Shops.CreateShop
{
    public sealed class CreateShopUseCase : AbstractUseCase<CreateShopInput, CreateShopOutput, ICreateShopOutputPort>
    {
        private readonly ShopsService _shopService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateShopUseCase(
            IUseCaseInputValidator<CreateShopInput> inputValidator,
            ICreateShopOutputPort output,
            ShopsService service,
            IUnitOfWork unitOfWork)
            : base(inputValidator, output)
        {
            _shopService = service ??
                throw new ArgumentNullException(nameof(service));
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override async Task Handle(CreateShopInput input)
        {
            var slug = Slug.Of(input.Name);

            var shopExists = await _shopService.ExistsAsync(slug);
            if (shopExists)
            {
                OutputPort.ShopAlreadyExists(input.Name);
                return;
            }

            PhoneNumber? phoneNumber = input.PhoneNumber.ToPhoneNumberOpt();
            Uri? websiteUrl = input.WebsiteUrl.ToUriOpt();
            MailAddress? mailAddress = input.EmailAddress.ToMailAddressOpt();

            var address = Address.TryCreate(
                input.Address?.Line1,
                input.Address?.Line2,
                input.Address?.City,
                input.Address?.Region,
                input.Address?.PostalCode,
                input.Address?.Country,
                out var a) ? a : (Address?)null;

            var id = await _shopService.CreateShopAsync(
                input.Name,
                websiteUrl,
                mailAddress,
                address,
                phoneNumber);

            var _ = await _unitOfWork.SaveAsync();

            OutputPort.Standard(new CreateShopOutput(slug));
        }
    }
}
