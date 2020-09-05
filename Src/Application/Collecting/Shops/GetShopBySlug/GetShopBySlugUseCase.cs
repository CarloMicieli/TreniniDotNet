using System;
using System.Threading.Tasks;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Collecting.Shops.GetShopBySlug
{
    public sealed class GetShopBySlugUseCase : AbstractUseCase<GetShopBySlugInput, GetShopBySlugOutput, IGetShopBySlugOutputPort>
    {
        private readonly ShopsService _shopService;

        public GetShopBySlugUseCase(IUseCaseInputValidator<GetShopBySlugInput> inputValidator, IGetShopBySlugOutputPort output, ShopsService service)
            : base(inputValidator, output)
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
