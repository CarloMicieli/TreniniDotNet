using MediatR;
using System;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetWishlistById
{
    public sealed class GetWishlistByIdRequest : IRequest
    {
        public Guid Id { get; set; }
        public string? Owner { get; set; }
    }
}
