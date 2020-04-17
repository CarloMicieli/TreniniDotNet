using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.EditWishlistItem;

namespace TreniniDotNet.Application.UseCases.Collection.Wishlists
{
    public sealed class EditWishlistItem : ValidatedUseCase<EditWishlistItemInput, IEditWishlistItemOutputPort>, IEditWishlistItemUseCase
    {
        public EditWishlistItem(IEditWishlistItemOutputPort output)
            : base(new EditWishlistItemInputValidator(), output)
        {
        }

        protected override Task Handle(EditWishlistItemInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
