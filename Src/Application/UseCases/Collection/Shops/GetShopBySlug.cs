using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.GetShopBySlug;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.Shops;

namespace TreniniDotNet.Application.UseCases.Collection.Shops
{
    public sealed class GetShopBySlug : ValidatedUseCase<GetShopBySlugInput, IGetShopBySlugOutputPort>, IGetShopBySlugUseCase
    {
        private readonly ShopsService _shopService;

        public GetShopBySlug(IGetShopBySlugOutputPort output, ShopsService service)
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
