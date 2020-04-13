using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.CreateWishlist;

namespace TreniniDotNet.Application.UseCases.Collection
{
    public sealed class CreateWishlist : ValidatedUseCase<CreateWishlistInput, ICreateWishlistOutputPort>, ICreateWishlistUseCase
    {
        public CreateWishlist(ICreateWishlistOutputPort output)
            : base(new CreateWishlistInputValidator(), output)
        {
        }

        protected override Task Handle(CreateWishlistInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
