using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.GetShopsList;
using TreniniDotNet.Domain.Collection.Shops;

namespace TreniniDotNet.Application.UseCases.Collection.Shops
{
    public sealed class GetShopsList : ValidatedUseCase<GetShopsListInput, IGetShopsListOutputPort>, IGetShopsListUseCase
    {
        private readonly ShopsService _shopsService;

        public GetShopsList(IGetShopsListOutputPort output, ShopsService shopsService)
            : base(new GetShopsListInputValidator(), output)
        {
            _shopsService = shopsService ??
                throw new ArgumentNullException(nameof(shopsService));
        }

        protected override async Task Handle(GetShopsListInput input)
        {
            var shops = await _shopsService.GetShopsAsync(input.Page);

            var output = new GetShopsListOutput(shops);
            OutputPort.Standard(output);
        }
    }
}
