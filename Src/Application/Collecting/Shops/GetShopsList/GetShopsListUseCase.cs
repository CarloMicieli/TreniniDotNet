using System;
using System.Threading.Tasks;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Domain.Collecting.Shops;

namespace TreniniDotNet.Application.Collecting.Shops.GetShopsList
{
    public sealed class GetShopsListUseCase : ValidatedUseCase<GetShopsListInput, IGetShopsListOutputPort>, IGetShopsListUseCase
    {
        private readonly ShopsService _shopsService;

        public GetShopsListUseCase(IGetShopsListOutputPort output, ShopsService shopsService)
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
