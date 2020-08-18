using System;
using System.Threading.Tasks;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Shops;

namespace TreniniDotNet.Application.Collecting.Shops.GetFavouriteShops
{
    public sealed class GetFavouriteShopsUseCase : AbstractUseCase<GetFavouriteShopsInput, GetFavouriteShopsOutput, IGetFavouriteShopsOutputPort>
    {
        private readonly ShopsService _shopsService;

        public GetFavouriteShopsUseCase(
            IUseCaseInputValidator<GetFavouriteShopsInput> inputValidator,
            IGetFavouriteShopsOutputPort outputPort,
            ShopsService shopsService)
            : base(inputValidator, outputPort)
        {
            _shopsService = shopsService ?? throw new ArgumentNullException(nameof(shopsService));
        }

        protected override async Task Handle(GetFavouriteShopsInput input)
        {
            var owner = new Owner(input.Owner);
            var favouriteShops = await _shopsService.GetFavouriteShops(owner);

            OutputPort.Standard(new GetFavouriteShopsOutput(favouriteShops));
        }
    }
}
