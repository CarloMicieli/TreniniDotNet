using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.GetWishlistsList;

namespace TreniniDotNet.Application.UseCases.Collection
{
    public sealed class GetWishlistsList : ValidatedUseCase<GetWishlistsListInput, IGetWishlistsListOutputPort>, IGetWishlistsListUseCase
    {
        public GetWishlistsList(IGetWishlistsListOutputPort output)
            : base(new GetWishlistsListInputValidator(), output)
        {
        }

        protected override Task Handle(GetWishlistsListInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
