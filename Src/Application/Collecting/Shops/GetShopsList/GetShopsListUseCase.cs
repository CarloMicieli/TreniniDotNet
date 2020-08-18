using System;
using System.Threading.Tasks;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Collecting.Shops;

namespace TreniniDotNet.Application.Collecting.Shops.GetShopsList
{
    public sealed class GetShopsListUseCase : AbstractUseCase<GetShopsListInput, GetShopsListOutput, IGetShopsListOutputPort>
    {
        private readonly ShopsService _shopsService;

        public GetShopsListUseCase(
            IUseCaseInputValidator<GetShopsListInput> inputValidator,
            IGetShopsListOutputPort output,
            ShopsService shopsService)
            : base(inputValidator, output)
        {
            _shopsService = shopsService ??
                throw new ArgumentNullException(nameof(shopsService));
        }

        protected override async Task Handle(GetShopsListInput input)
        {
            var shops = await _shopsService.GetAllShopsAsync(input.Page);

            var output = new GetShopsListOutput(shops);
            OutputPort.Standard(output);
        }
    }
}
