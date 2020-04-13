using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.GetWishlistById;

namespace TreniniDotNet.Application.UseCases.Collection
{
    public sealed class GetWishlistById : ValidatedUseCase<GetWishlistByIdInput, IGetWishlistByIdOutputPort>, IGetWishlistByIdUseCase
    {
        public GetWishlistById(IGetWishlistByIdOutputPort output)
            : base(new GetWishlistByIdInputValidator(), output)
        {
        }

        protected override Task Handle(GetWishlistByIdInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
