using FluentValidation;
using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.RemoveItemFromWishlist;

namespace TreniniDotNet.Application.UseCases.Collection
{
    public sealed class RemoveItemFromWishlist : ValidatedUseCase<RemoveItemFromWishlistInput, IRemoveItemFromWishlistOutputPort>, IRemoveItemFromWishlistUseCase
    {
        public RemoveItemFromWishlist(IRemoveItemFromWishlistOutputPort output)
            : base(new RemoveItemFromWishlistInputValidator(), output)
        {
        }

        protected override Task Handle(RemoveItemFromWishlistInput input)
        {
            throw new NotImplementedException();
        }
    }
}
