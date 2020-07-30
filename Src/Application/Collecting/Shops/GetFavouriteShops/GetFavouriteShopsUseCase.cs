using System.Threading.Tasks;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;

namespace TreniniDotNet.Application.Collecting.Shops.GetFavouriteShops
{
    public sealed class GetFavouriteShopsUseCase : AbstractUseCase<GetFavouriteShopsInput, GetFavouriteShopsOutput, IGetFavouriteShopsOutputPort>
    {
        public GetFavouriteShopsUseCase(
            IUseCaseInputValidator<GetFavouriteShopsInput> inputValidator,
            IGetFavouriteShopsOutputPort outputPort)
            : base(inputValidator, outputPort)
        {
        }

        protected override Task Handle(GetFavouriteShopsInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
