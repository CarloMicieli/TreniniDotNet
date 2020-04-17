using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.AddItemToWishlist;

namespace TreniniDotNet.Application.UseCases.Collection.Wishlists
{
    public sealed class AddItemToWishlist : ValidatedUseCase<AddItemToWishlistInput, IAddItemToWishlistOutputPort>, IAddItemToWishlistUseCase
    {
        public AddItemToWishlist(IAddItemToWishlistOutputPort output)
            : base(new AddItemToWishlistInputValidator(), output)
        {
        }

        protected override Task Handle(AddItemToWishlistInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
