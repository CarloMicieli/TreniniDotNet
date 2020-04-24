using MediatR;
using System;

namespace TreniniDotNet.Web.UseCases.V1.Collection.DeleteWishlist
{
    public sealed class DeleteWishlistRequest : IRequest
    {
        public Guid Id { get; set; }
        public string? Owner { get; set; }
    }
}
