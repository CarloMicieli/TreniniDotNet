using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Application.Collecting.Wishlists.CreateWishlist
{
    public sealed class CreateWishlistUseCase : ValidatedUseCase<CreateWishlistInput, ICreateWishlistOutputPort>, ICreateWishlistUseCase
    {
        private readonly WishlistService _wishlistService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateWishlistUseCase(ICreateWishlistOutputPort output, WishlistService wishlistService, IUnitOfWork unitOfWork)
            : base(new CreateWishlistInputValidator(), output)
        {
            _wishlistService = wishlistService ??
                throw new ArgumentNullException(nameof(wishlistService));
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override async Task Handle(CreateWishlistInput input)
        {
            var owner = new Owner(input.Owner);
            var slug = Slug.Of(input.ListName);

            var alreadyExist = await _wishlistService.ExistAsync(owner, slug);
            if (alreadyExist)
            {
                OutputPort.WishlistAlreadyExists(slug);
                return;
            }

            var visibility = EnumHelpers.RequiredValueFor<Visibility>(input.Visibility);

            var listId = await _wishlistService.CreateWishlist(owner, slug, input.ListName, visibility);

            var _ = await _unitOfWork.SaveAsync();

            OutputPort.Standard(new CreateWishlistOutput(listId, slug));
        }
    }
}
