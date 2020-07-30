using System;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Collecting.Wishlists.CreateWishlist
{
    public sealed class CreateWishlistUseCase : AbstractUseCase<CreateWishlistInput, CreateWishlistOutput, ICreateWishlistOutputPort>
    {
        private readonly WishlistsService _wishlistService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateWishlistUseCase(
            IUseCaseInputValidator<CreateWishlistInput> inputValidator,
            ICreateWishlistOutputPort output,
            WishlistsService wishlistService,
            IUnitOfWork unitOfWork)
            : base(inputValidator, output)
        {
            _wishlistService = wishlistService ??
                throw new ArgumentNullException(nameof(wishlistService));
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override async Task Handle(CreateWishlistInput input)
        {
            var owner = new Owner(input.Owner);

            var listName = await _wishlistService.GenerateWishlistName(owner, input.ListName);
            var slug = Slug.Of(listName);

            var alreadyExist = await _wishlistService.ExistsAsync(owner, listName);
            if (alreadyExist)
            {
                OutputPort.WishlistAlreadyExists(slug);
                return;
            }

            var visibility = EnumHelpers.RequiredValueFor<Visibility>(input.Visibility);
            var budget = input.Budget?.ToBudgetOrDefault();

            var listId = await _wishlistService.CreateWishlistAsync(owner, input.ListName, visibility, budget);

            var _ = await _unitOfWork.SaveAsync();

            OutputPort.Standard(new CreateWishlistOutput(listId, slug));
        }
    }
}
