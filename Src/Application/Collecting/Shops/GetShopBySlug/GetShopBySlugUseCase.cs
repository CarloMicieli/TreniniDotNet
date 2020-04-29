using System;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Domain.Collecting.Shops;

namespace TreniniDotNet.Application.Collecting.Shops.GetShopBySlug
{
    public sealed class GetShopBySlugUseCase : ValidatedUseCase<GetShopBySlugInput, IGetShopBySlugOutputPort>, IGetShopBySlugUseCase
    {
        private readonly ShopsService _shopService;

        public GetShopBySlugUseCase(IGetShopBySlugOutputPort output, ShopsService service)
            : base(new GetShopBySlugInputValidator(), output)
        {
            _shopService = service ??
                throw new ArgumentNullException(nameof(service));
        }

        protected override async Task Handle(GetShopBySlugInput input)
        {
            var slug = Slug.Of(input.Slug);

            var shop = await _shopService.GetBySlugAsync(slug);
            if (shop is null)
            {
                OutputPort.ShopNotFound(slug);
                return;
            }

            var output = new GetShopBySlugOutput(shop);
            OutputPort.Standard(output);
        }
    }
}
