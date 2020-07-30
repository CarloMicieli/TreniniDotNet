using System;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Application.Collecting.Wishlists.DeleteWishlist
{
    public sealed class DeleteWishlistUseCase : AbstractUseCase<DeleteWishlistInput, DeleteWishlistOutput, IDeleteWishlistOutputPort>
    {
        private readonly WishlistsService _wishlistService;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteWishlistUseCase(
            IUseCaseInputValidator<DeleteWishlistInput> inputValidator,
            IDeleteWishlistOutputPort outputPort,
            WishlistsService wishlistService,
            IUnitOfWork unitOfWork)
            : base(inputValidator, outputPort)
        {
            _wishlistService = wishlistService ??
                throw new ArgumentNullException(nameof(wishlistService));
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override async Task Handle(DeleteWishlistInput input)
        {
            var id = new WishlistId(input.Id);
            var owner = new Owner(input.Owner);

            var wishlistExists = await _wishlistService.ExistsAsync(id);
            if (wishlistExists == false)
            {
                OutputPort.WishlistNotFound(id);
                return;
            }

            await _wishlistService.DeleteWishlistAsync(id);

            var _ = await _unitOfWork.SaveAsync();

            OutputPort.Standard(new DeleteWishlistOutput(id));
        }
    }
}
