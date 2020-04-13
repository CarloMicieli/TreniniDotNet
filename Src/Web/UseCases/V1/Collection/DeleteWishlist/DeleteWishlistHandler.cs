using AutoMapper;
using TreniniDotNet.Application.Boundaries.Collection.DeleteWishlist;

namespace TreniniDotNet.Web.UseCases.V1.Collection.DeleteWishlist
{
    public sealed class DeleteWishlistHandler : UseCaseHandler<IDeleteWishlistUseCase, DeleteWishlistRequest, DeleteWishlistInput>
    {
        public DeleteWishlistHandler(IDeleteWishlistUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
