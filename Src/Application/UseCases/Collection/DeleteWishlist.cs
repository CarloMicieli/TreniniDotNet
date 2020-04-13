using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.DeleteWishlist;

namespace TreniniDotNet.Application.UseCases.Collection
{
    public sealed class DeleteWishlist : ValidatedUseCase<DeleteWishlistInput, IDeleteWishlistOutputPort>, IDeleteWishlistUseCase
    {
        public DeleteWishlist(IDeleteWishlistOutputPort output)
            : base(new DeleteWishlistInputValidator(), output)
        {
        }

        protected override Task Handle(DeleteWishlistInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
