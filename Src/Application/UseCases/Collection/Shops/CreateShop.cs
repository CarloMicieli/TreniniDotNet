using System;
using System.Net.Mail;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.CreateShop;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Addresses;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Common.PhoneNumbers;
using TreniniDotNet.Domain.Collection.Shops;

namespace TreniniDotNet.Application.UseCases.Collection.Shops
{
    public sealed class CreateShop : ValidatedUseCase<CreateShopInput, ICreateShopOutputPort>, ICreateShopUseCase
    {
        private readonly ShopsService _shopService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateShop(ICreateShopOutputPort output, ShopsService service, IUnitOfWork unitOfWork)
            : base(new CreateShopInputValidator(), output)
        {
            _shopService = service ??
                throw new ArgumentNullException(nameof(service));
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override async Task Handle(CreateShopInput input)
        {
            var slug = Slug.Of(input.Name);

            bool shopExists = await _shopService.ExistsAsync(slug);
            if (shopExists)
            {
                OutputPort.ShopAlreadyExists(input.Name);
                return;
            }

            PhoneNumber? phoneNumber = input.PhoneNumber.ToPhoneNumberOpt();
            Uri? websiteUrl = input.WebsiteUrl.ToUriOpt();
            MailAddress? mailAddress = input.EmailAddress.ToMailAddressOpt();

            Address? address = Address.TryCreate(
                input.Address?.Line1,
                input.Address?.Line2,
                input.Address?.City,
                input.Address?.Region,
                input.Address?.PostalCode,
                input.Address?.Country,
                out var a) ? a : (Address?)null;

            var shopId = await _shopService.CreateShop(
                input.Name,
                websiteUrl,
                mailAddress,
                address,
                phoneNumber);

            var _ = await _unitOfWork.SaveAsync();

            OutputPort.Standard(new CreateShopOutput(shopId, slug));
        }
    }
}
